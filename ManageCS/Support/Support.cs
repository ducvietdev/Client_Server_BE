using ManageCS.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace ManageCS.Support
{
    public class Support
    {
        public string ipServer = "http://127.0.0.1";
        ManageCSContext _context = new ManageCSContext();

        public string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        //public string GetFileExtension(string s)
        //{
        //    string[] Name_extension = s.Split('.');
        //    int countarray = Name_extension.Count();
        //    s = Name_extension[countarray - 1];
        //    return s;
        //}

        //public string GetIPv4InPC()
        //{
        //    string hostName = Dns.GetHostName(); // Lấy tên máy tính
        //    IPHostEntry hostEntry = Dns.GetHostEntry(hostName); // Lấy thông tin về máy tính
        //    string ip = "127.0.0.1";
        //    foreach (IPAddress ipAddress in hostEntry.AddressList)
        //    {
        //        if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
        //        {
        //            ip = ipAddress.ToString();
        //        }
        //    }
        //    return ip;
        //}

        public bool CheckIsEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            bool isValidEmail = Regex.IsMatch(email, pattern);
            return isValidEmail;
        }

        public DateTime ConvertToDateTime(string input)
        {
            string format = "yyyy-MM-dd h:mm:ss:tt";

            if (DateTime.TryParseExact(input, format, null, System.Globalization.DateTimeStyles.None, out DateTime result))
            {
                return result;
            }
            else
            {
                return DateTime.Now;
            }
        }
        static bool IsRunningAsAdmin()
        {
            using (var identity = System.Security.Principal.WindowsIdentity.GetCurrent())
            {
                var principal = new System.Security.Principal.WindowsPrincipal(identity);
                return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
            }
        }

        //Khởi động lại chương trình với quyền Administrator
        //static void StartAsAdmin()
        //{
        //    var startInfo = new ProcessStartInfo();
        //    startInfo.UseShellExecute = true;
        //    startInfo.WorkingDirectory = Environment.CurrentDirectory;
        //    startInfo.FileName = System.Reflection.Assembly.GetEntryAssembly().Location;
        //    startInfo.Verb = "runas"; // Khởi động lại với quyền Administrator

        //    try
        //    {
        //        Process.Start(startInfo);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Lỗi: " + ex.Message);
        //    }
        //}
        public bool roleAdmin(string? id)
        {
            if (id == null) return false;
            else
            {
                var admin = _context.UserLogins.FirstOrDefault(m => m.Id == id);
                if (admin.RoleName == "Admin") { return true; }
                else return false;
            }
        }
        //public void sendEmail(string name, string receiver, string linkUrl)
        //{
        //    string sender = "baodepbaobao@gmail.com";
        //    string pass = "xgdenrcsaftrocjo";
        //    MailMessage mail = new MailMessage();
        //    mail.From = new MailAddress(sender);
        //    mail.To.Add(new MailAddress(receiver));
        //    mail.Subject = "Đặt lại mật khẩu hệ thống LMS";

        //    // Creating the link using an anchor tag
        //    string linkText = " tại đây";
        //    string linkHtml = $"<br><a>Đổi mật khẩu</a><a href='{linkUrl}'>{linkText}</a>";

        //    mail.Body = $"Tài khoản {name} của hệ thống Learning Management System đã được yêu cầu thay đổi mật khẩu. Nếu đây là bạn, vui lòng sử dụng liên kết sau để đặt lại mật khẩu của bạn. {linkHtml}";
        //    mail.IsBodyHtml = true; // Set to true since the body contains HTML
        //    mail.Priority = MailPriority.Normal;

        //    SmtpClient smtp = new SmtpClient("smtp.gmail.com");
        //    smtp.EnableSsl = true;
        //    smtp.UseDefaultCredentials = false;
        //    smtp.Port = 587;
        //    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    smtp.Credentials = new NetworkCredential(sender, pass);
        //    smtp.Send(mail);
        //}
    }
}
