using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace UpdateSVG
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = folderBrowserDialog1.ShowDialog();
            string filePath = "";
            if (result == DialogResult.OK)
            {
                filePath = folderBrowserDialog1.SelectedPath;
                string pf = filePath.Substring(0, filePath.LastIndexOf('\\'));
                if (pf.EndsWith(":"))
                {
                    MessageBox.Show("请选择文件目录，不能直接选择盘符根目录。");
                    return;
                }
                this.textBox1.Text = filePath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string dirpath = this.textBox1.Text;
            if (Directory.Exists(dirpath))
            {
                //DirectoryInfo dir = new DirectoryInfo(dirpath);
                //FileInfo[] svgfile = dir.GetFiles("*.svg");
                String[] svgfile = Directory.GetFiles(dirpath, "*.svg", SearchOption.AllDirectories);
                foreach (string file in svgfile)
                {
                    FileStream fs = null;
                    StreamReader sr = null;
                    FileStream ws = null;
                    try
                    {
                        fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                        sr = new StreamReader(fs, Encoding.GetEncoding("utf-8"));
                        string line = sr.ReadToEnd();
                        line = line.Replace("户名", "hm");
                        line = line.Replace("255,220,190", "255,255,255");                        
                        fs.Close();
                        fs = null;
                        File.Delete(file);

                        ws = new FileStream(file, FileMode.Create);
                        //获得字节数组
                        byte[] data = System.Text.Encoding.UTF8.GetBytes(line);
                        //开始写入
                        ws.Write(data, 0, data.Length);
                        //清空缓冲区、关闭流
                        ws.Flush();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        if (fs != null)
                        {
                            fs.Close();
                        }
                        if (sr != null)
                        {
                            sr.Close();
                        }
                         if (ws != null)
                        {
                            ws.Close();
                        }
                    }
                        

                }
            }
            else
            {
                MessageBox.Show("文件目录不正确。");
                return;
            }
        }
    }
}
