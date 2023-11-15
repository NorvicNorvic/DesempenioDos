using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DesempenioDos
{
    public partial class Registracion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = new HttpCookie("pass", this.txtPass.Text);
            cookie.Expires = new DateTime(2023, 12, 31);
            this.Response.Cookies.Add(cookie);

            this.Session["usuario"]=this.txtUsername.Text;
        }
    }
}