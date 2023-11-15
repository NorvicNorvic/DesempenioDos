using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;

namespace DesempenioDos
{
    public partial class Archivos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarGrilla();
        }

        public void cargarGrilla()
        {
            string path = "";
            if ((this.Session["usuario"] != null))
            {
                path = $"{Server.MapPath(".")}\\{this.Session["usuario"]}";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string[] files = Directory.GetFiles(path);
                List<Archivo> fileList = new List<Archivo>();
                foreach (string file in files)
                {
                    var fileNew = new Archivo(Path.GetFileName(file), file);
                    fileList.Add(fileNew);
                }
                GridView1.DataSource = fileList;

            }
            
                GridView1.DataBind();
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string path = "";
            if ((this.Session["usuario"] != null))
            {
                path = $"{Server.MapPath(".")}\\{this.Session["usuario"]}";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }


            }


            string result = string.Empty;
            if ((this.Session["usuario"] != null))
            {
                foreach (HttpPostedFile archivo in FileUpload1.PostedFiles)
            {
                if (archivo.ContentLength > 100000)
                {
                    result += $"El archivo {archivo.FileName} supera los 10000 bytes - ";
                }
                else
                {
                    if (File.Exists($"{path}/{archivo.FileName}"))
                    {
                        result += $"El archivo {archivo.FileName} ya existe - ";
                    }
                    else
                    {
                        FileUpload1.SaveAs($"{path}/{archivo.FileName}");
                    }
                }

            }

            }  else { result = "No se inició la sesión"; }

            Label1.Text = result;
            cargarGrilla();
        }


        protected void GridView1_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Dowload")
            {
                GridViewRow registro = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
                string filePath = registro.Cells[2].Text;

                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                Response.TransmitFile(filePath);
                Response.End();
            }
        }


    }
}