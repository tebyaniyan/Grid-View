using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing;


public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
protected void btnUpload_Click(object sender, EventArgs e)
{
 string strImageName = txtName.Text.ToString();
 if (FileUpload1.PostedFile != null && FileUpload1.PostedFile.FileName != "")
 {
    string strExtension = System.IO.Path.GetExtension(FileUpload1.FileName);
    if ((strExtension.ToUpper() == ".JPG") | (strExtension.ToUpper() == ".GIF"))
    {
     // Resize Image Before Uploading to DataBase
      System.Drawing.Image imageToBeResized = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);
      int imageHeight = imageToBeResized.Height;
      int imageWidth = imageToBeResized.Width;
      int maxHeight = 240;
      int maxWidth = 320;
      imageHeight = (imageHeight * maxWidth) / imageWidth;
      imageWidth = maxWidth;

              if (imageHeight > maxHeight)
                {
                    imageWidth = (imageWidth * maxHeight) / imageHeight;
                    imageHeight = maxHeight;
                }

                Bitmap bitmap = new Bitmap(imageToBeResized, imageWidth, imageHeight);
                System.IO.MemoryStream stream = new MemoryStream();
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                stream.Position = 0;
                byte[] image = new byte[stream.Length + 1];
                stream.Read(image, 0, image.Length);



                // Create SQL Connection 
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                // Create SQL Command 

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "INSERT INTO Images(ImageName,Image) VALUES (@ImageName,@Image)";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                SqlParameter ImageName = new SqlParameter("@ImageName", SqlDbType.VarChar, 50);
                ImageName.Value = strImageName.ToString();
                cmd.Parameters.Add(ImageName);

                SqlParameter UploadedImage = new SqlParameter("@Image", SqlDbType.Image, image.Length);
                UploadedImage.Value = image;
                cmd.Parameters.Add(UploadedImage);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                con.Close();
                if (result > 0)
                    lblMessage.Text = "File Uploaded";
                GridView1.DataBind();
            }
        }
    }
}
