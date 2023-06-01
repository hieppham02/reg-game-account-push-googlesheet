using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xNet;
using Newtonsoft;
using System.IO;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System.Security.Cryptography;
using System.Threading;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Policy;
using Microsoft.VisualBasic;
using IronBarCode;
using IronOcr;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace Cwin_version_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.ScrollBars = ScrollBars.Vertical;
            this.StartPosition = FormStartPosition.CenterScreen;
            textBox3.Text = File.ReadAllText("Proxy.txt");
        }
        private Random rd = new Random();
        private SaveToken token = new SaveToken();
        List<IWebDriver> listChrome = new List<IWebDriver>();
        public void getCaptchaWin55()
        {
            string proxy_text = textBox3.Text;
            string[] array_list = proxy_text.Split(':');
            if (array_list.Length != 2)
            {
                // Xử lý lỗi khi không đúng định dạng proxy
                return;
            }
            int proxyPort;
            if (!int.TryParse(array_list[1], out proxyPort))
            {
                // Xử lý lỗi khi không thể chuyển đổi cổng sang số
                return;
            }
            HttpProxyClient proxy = new HttpProxyClient(array_list[0], proxyPort);
            string[] user_agent = File.ReadAllLines("Desktop.txt");
            int userAgentIndex = rd.Next(0, user_agent.Length);
            using (HttpRequest request = new HttpRequest())
            {
                request.AddHeader("user-agent", user_agent[userAgentIndex]);
                request.Proxy = proxy;
                int randomTime = rd.Next(111111, 999999);
                byte[] img_res = request.Get($"https://www.137722.cc/api/getCaptcha2?time=1685207{randomTime}").ToBytes();
                using (var ms = new MemoryStream(img_res))
                {
                    pictureBox1.Image = System.Drawing.Image.FromStream(ms);
                }
            }
            File.WriteAllText("Proxy.txt", textBox3.Text);
        }
        public void getCaptchaCwin()
        {
            string proxy_text = textBox3.Text;
            string[] array_list = proxy_text.Split(':');
            if (array_list.Length != 2)
            {
                // Xử lý lỗi khi không đúng định dạng proxy
                return;
            }
            int proxyPort;
            if (!int.TryParse(array_list[1], out proxyPort))
            {
                // Xử lý lỗi khi không thể chuyển đổi cổng sang số
                return;
            }
            HttpProxyClient proxy = new HttpProxyClient(array_list[0], proxyPort);
            string[] user_agent = File.ReadAllLines("Desktop.txt");
            int userAgentIndex = rd.Next(0, user_agent.Length);
            using (HttpRequest request = new HttpRequest())
            {
                request.AddHeader("user-agent", user_agent[userAgentIndex]);
                request.Proxy = proxy;
                int randomTime = rd.Next(111111, 999999);
                byte[] img_res = request.Get($"https://www.110cwin.com/api/getCaptcha2?time={randomTime}").ToBytes();
                using (var ms = new MemoryStream(img_res))
                {
                    pictureBox1.Image = System.Drawing.Image.FromStream(ms);
                }
            }
            File.WriteAllText("Proxy.txt", textBox3.Text);
        }
        public void checkWin55(string phone_number, int i)
        {
            int code;
            if (!Int32.TryParse(textBox2.Text, out code))
            {
                // Xử lý lỗi khi textBox3 không chứa một giá trị số nguyên hợp lệ
                MessageBox.Show("Captcha không xác định");
                return;
            }

            string[] proxy_array = textBox3.Text.Split(':');
            if (proxy_array.Length != 2)
            {
                // Xử lý lỗi khi textBox2 không chứa chuỗi "host:port" hợp lệ
                MessageBox.Show("Proxy không xác định");
                return;
            }

            string[] user_agent;
            try
            {
                user_agent = File.ReadAllLines("Desktop.txt");
            }
            catch (FileNotFoundException)
            {
                // Xử lý lỗi khi tệp "ua.txt" không tồn tại
                MessageBox.Show("Thiếu file Desktop.txt");
                return;
            }


            SendMobileRequest sendMobileRequest = new SendMobileRequest();
            HttpRequest httpRequest = new HttpRequest();
            HttpProxyClient proxy = new HttpProxyClient(proxy_array[0], Int32.Parse(proxy_array[1]));
            httpRequest.Proxy = proxy;
            Random random_ua = new Random();
            int index = random_ua.Next(user_agent.Length);
            string ua = user_agent[index];
            httpRequest.UserAgent = ua;
            httpRequest.AddHeader("Accept", "application/json, text/plain, */*");
            httpRequest.AddHeader("Accept-Language", "en-US,en;q=0.9");
            httpRequest.AddHeader("Origin", "https://www.win5508.com");
            httpRequest.AddHeader("Referer", "https://www.win5508.com/");
            string response = httpRequest.Get("https://www.137722.cc/api/send?mobile=" + phone_number + "&catcph=" + code).ToString();
            DateTime time = DateTime.Now;
            string fileName = time.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                sendMobileRequest = JsonConvert.DeserializeObject<SendMobileRequest>(response);
            }
            catch (JsonException)
            {
                // Xử lý lỗi khi dữ liệu JSON không được phân tích đúng
                MessageBox.Show("Error parsing JSON response");
                return;
            }
            string message;
            if (sendMobileRequest.message == "Tần suất yêu cầu quá cao")
            {
                message = "Hết lượt check, đợi 1 lúc rồi check lại";
                File.AppendAllText("checklai.txt", $"{phone_number}|{sendMobileRequest.message}{"\n"}");
            }
            else if (sendMobileRequest.message == "Số điện thoại di động của bạn đã không được mời, cảm ơn bạn!")
            {
                message = "Sim không đủ điều kiện";
                File.AppendAllText("khongdudk.txt", $"{fileName}|{sendMobileRequest}|{sendMobileRequest.message}{"\n"}");
            }
            else if (sendMobileRequest.message == "Gửi thành công")
            {
                message = "Thành công";
                File.AppendAllText("thanhcong.txt", $"{fileName}|{sendMobileRequest} | {sendMobileRequest.message}{"\n"}");
            }
            else
            {
                message = sendMobileRequest.message;
                File.AppendAllText("ketqua.txt", $"{fileName}|{sendMobileRequest} | {sendMobileRequest.message}{"\n"}");
            }
            string[] data = { i.ToString(), phone_number, sendMobileRequest.message };
            dataGridView1.Invoke(new Action(() =>
            {
                dataGridView1.Rows.Add(data);
                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
            }));

            // Hiển thị thông báo
            //MessageBox.Show(sendMobileRequest.message);
        }
        public void checkCwin(string phone_number, int i)
        {
            int code;
            if (!Int32.TryParse(textBox2.Text, out code))
            {
                // Xử lý lỗi khi textBox3 không chứa một giá trị số nguyên hợp lệ
                MessageBox.Show("Captcha không xác định");
                return;
            }

            string[] proxy_array = textBox3.Text.Split(':');
            if (proxy_array.Length != 2)
            {
                // Xử lý lỗi khi textBox2 không chứa chuỗi "host:port" hợp lệ
                MessageBox.Show("Proxy không xác định");
                return;
            }

            string[] user_agent;
            try
            {
                user_agent = File.ReadAllLines("Desktop.txt");
            }
            catch (FileNotFoundException)
            {
                // Xử lý lỗi khi tệp "ua.txt" không tồn tại
                MessageBox.Show("Thiếu file Desktop.txt");
                return;
            }


            SendMobileRequest sendMobileRequest = new SendMobileRequest();
            HttpRequest httpRequest = new HttpRequest();
            HttpProxyClient proxy = new HttpProxyClient(proxy_array[0], Int32.Parse(proxy_array[1]));
            httpRequest.Proxy = proxy;
            Random random_ua = new Random();
            int index = random_ua.Next(user_agent.Length);
            string ua = user_agent[index];
            httpRequest.UserAgent = ua;
            httpRequest.AddHeader("Accept", "application/json, text/plain, */*");
            httpRequest.AddHeader("Accept-Language", "en-US,en;q=0.9");
            httpRequest.AddHeader("Origin", "https://www.win5508.com");
            httpRequest.AddHeader("Referer", "https://www.win5508.com/");
            string response = httpRequest.Get("https://www.110cwin.com/api/send?mobile=" + phone_number + "&catcph=" + code).ToString();
            DateTime time = DateTime.Now;
            string fileName = time.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                sendMobileRequest = JsonConvert.DeserializeObject<SendMobileRequest>(response);
            }
            catch (JsonException)
            {
                // Xử lý lỗi khi dữ liệu JSON không được phân tích đúng
                MessageBox.Show("Error parsing JSON response");
                return;
            }
            string message;
            if (sendMobileRequest.message == "Tần suất yêu cầu quá cao")
            {
                message = "Hết lượt check, đợi 1 lúc rồi check lại";
                File.AppendAllText("checklai.txt", $"{phone_number}|{sendMobileRequest.message}{"\n"}");
            }
            else if (sendMobileRequest.message == "Số điện thoại di động của bạn đã không được mời, cảm ơn bạn!")
            {
                message = "Sim không đủ điều kiện";
                File.AppendAllText("khongdudk.txt", $"{fileName}|{sendMobileRequest}|{sendMobileRequest.message}{"\n"}");
            }
            else if (sendMobileRequest.message == "Gửi thành công")
            {
                message = "Thành công";
                File.AppendAllText("thanhcong.txt", $"{fileName}|{sendMobileRequest} | {sendMobileRequest.message}{"\n"}");
            }
            else
            {
                message = sendMobileRequest.message;
                File.AppendAllText("ketqua.txt", $"{fileName}|{sendMobileRequest} | {sendMobileRequest.message}{"\n"}");
            }
            string[] data = { i.ToString(), phone_number, sendMobileRequest.message };
            dataGridView1.Invoke(new Action(() =>
            {
                dataGridView1.Rows.Add(data);
                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
            }));

            // Hiển thị thông báo
            //MessageBox.Show(sendMobileRequest.message);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getCaptchaWin55();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            new Thread(() =>
            {
                string[] phone_number = textBox1.Lines;
                int i = 1;
                foreach (string line in phone_number)
                {
                    checkWin55(line, i);
                    i++;
                }
            }).Start();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            nhan88kk(AccTextBox.Text, OtpTextBox.Text, SdtTextBox.Text);
        }
        public void nhan88kk(string user_name, string otp, string phone_number)
        {
            int code;
            if (!Int32.TryParse(textBox2.Text, out code))
            {
                // Xử lý lỗi khi textBox2 không chứa một giá trị số nguyên hợp lệ
                MessageBox.Show("Captcha không xác định");
                return;
            }
            string[] proxy_array = textBox3.Text.Split(':');
            if (proxy_array.Length != 2)
            {
                // Xử lý lỗi khi textBox3 không chứa chuỗi "host:port" hợp lệ
                MessageBox.Show("Proxy không xác định");
                return;
            }
            string[] user_agent;
            try
            {
                user_agent = File.ReadAllLines("Mobile.txt");
            }
            catch (FileNotFoundException)
            {
                // Xử lý lỗi khi tệp "ua.txt" không tồn tại
                MessageBox.Show("Thiếu file ua.txt");
                return;
            }
            SendMobileRequest sendMobileRequest = new SendMobileRequest();
            HttpRequest httpRequest = new HttpRequest();
            HttpProxyClient proxy = new HttpProxyClient(proxy_array[0], Int32.Parse(proxy_array[1]));
            httpRequest.Proxy = proxy;
            Random random_ua = new Random();
            int index = random_ua.Next(user_agent.Length);
            string ua = user_agent[index];
            httpRequest.UserAgent = ua;
            httpRequest.AddHeader("Accept", "application/json, text/plain, */*");
            httpRequest.AddHeader("Accept-Language", "en-US,en;q=0.9");
            httpRequest.AddHeader("Origin", "https://www.win5508.com");
            httpRequest.AddHeader("Referer", "https://www.win5508.com/");
            string response = httpRequest.Get("https://www.137722.cc/api/apply?mobile=" + phone_number + "&username=" + user_name + "&code=" + otp + "&catcph=" + code).ToString();
            try
            {
                sendMobileRequest = JsonConvert.DeserializeObject<SendMobileRequest>(response);
            }
            catch (JsonException)
            {
                // Xử lý lỗi khi dữ liệu JSON không được phân tích đúng
                MessageBox.Show("Error parsing JSON response");
                return;
            }
            // Hiển thị thông báo
            MessageBox.Show(sendMobileRequest.message);
            OtpTextBox.Text = " ";
            textBox2.Text = " ";
        }
        DataImage dataImage = new DataImage();
        private void button6_Click(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            string fileName = time.ToString("yy-MM-dd-hh-mm-ss");
            string[] inputdata = File.ReadAllLines("Data.txt");
            foreach (string line in inputdata)
            {
                Thread t = new Thread(() =>
                {
                    string[] data = line.Split('|');
                    DangkyTaikhoanWin55(data[0], data[1], data[2], data[3], data[4], data[5], CaptchaTextBox.Text, fileName);
                });
                t.Start();
            }

            MessageBox.Show("Xong, vui lòng kiểm tra file", "Thông báo", MessageBoxButtons.OK);
            CaptchaTextBox.Text = " ";
        }

        public void DangkyTaikhoanCwin(string sdt, string username, string pass, string moneypass, string name, string bankaccount, string captcha, string fileName)
        {

            token.AccessToken = null;
            token.RefreshToken = null;
            string proxy_text = textBox3.Text;
            string[] array_list = proxy_text.Split(':');
            if (array_list.Length != 2)
            {
                // Xử lý lỗi khi không đúng định dạng proxy
                return;
            }
            int proxyPort;
            if (!int.TryParse(array_list[1], out proxyPort))
            {
                // Xử lý lỗi khi không thể chuyển đổi cổng sang số
                return;
            }
            HttpProxyClient proxy = new HttpProxyClient(array_list[0], proxyPort);

            string[] user_agent = File.ReadAllLines("Mobile.txt");
            int userAgentIndex = rd.Next(0, user_agent.Length);
            using (HttpRequest request = new HttpRequest())
            {
                request.AddHeader("user-agent", user_agent[userAgentIndex]);
                request.Proxy = proxy;
                request.AddHeader("Accept", "application/json, text/plain, */*");
                request.AddHeader("Accept-Language", "en-US,en;q=0.9");
                request.AddHeader("Origin", "https://www.cwin0066.com");
                request.AddHeader("Referer", "https://www.cwin0066.com/");
                DataAccount account = new DataAccount()
                {
                    account = username,
                    bankAccount = null,
                    bankCity = null,
                    bankName = null,
                    bankProvince = null,
                    birthday = null,
                    checkCode = captcha,
                    checkCodeEncrypt = dataImage.value,
                    confirm_Password = pass,
                    countryCode = "84",
                    dealerAccount = null,
                    email = null,
                    groupBank = null,
                    idNumber = null,
                    isRequiredMoneyPassword = false,
                    mobile = null,
                    moneyPassword = null,
                    name = name,
                    parentAccount = "dx673",
                    password = pass,
                    qqAccount = null,
                    sex = null
                };
                string payload = JsonConvert.SerializeObject(account);
                string response = request.Post("https://www.cwin0066.com/api/1.0/member/register", payload, "application/json; charset=utf-8").ToString();
                string pattern_1 = "\"AccessToken\":\"([^\"]*)\"";
                Match match_1 = Regex.Match(response, pattern_1);
                JObject jsonObject = JObject.Parse(response);
                int code = (int)jsonObject["Code"];
                //MessageBox.Show(code.ToString());
                if (code == 200)
                {
                    if (match_1.Success)
                    {
                        // Lấy giá trị AccessToken từ match.Groups[1]
                        string accessToken = match_1.Groups[1].Value;
                        request.Proxy = proxy;
                        request.AddHeader("user-agent", user_agent[userAgentIndex]);
                        request.AddHeader("Authorization", "Bearer " + accessToken);
                        request.AddHeader("Accept", "application/json, text/plain, */*");
                        request.AddHeader("Accept-Language", "en-US,en;q=0.9");
                        request.AddHeader("Origin", "https://www.cwin0066.com");
                        request.AddHeader("Referer", "https://www.cwin0066.com/");
                        SetMoneyPass setMoneyPass = new SetMoneyPass()
                        {
                            isCreate = true,
                            newPassword = moneypass,
                            oldPassword = null,
                        };
                        string setPassPayload = JsonConvert.SerializeObject(setMoneyPass);
                        string setPassResponse = request.Post("https://www.cwin0066.com/api/0.0/Account/ChangeMoneyPasswordSubmit", setPassPayload, "application/json; charset=utf-8").ToString();
                        if (setPassResponse == "{\"IsSuccess\":true,\"ErrorMessage\":null}")
                        {
                            request.Proxy = proxy;
                            request.AddHeader("user-agent", user_agent[userAgentIndex]);
                            request.AddHeader("Authorization", "Bearer " + accessToken);
                            request.AddHeader("Accept", "application/json, text/plain, */*");
                            request.AddHeader("Accept-Language", "en-US,en;q=0.9");
                            request.AddHeader("Origin", "https://www.cwin0066.com");
                            request.AddHeader("Referer", "https://www.cwin0066.com/");
                            DataBank dataBank = new DataBank()
                            {
                                account = bankaccount,
                                city = "DIEN BIEN PHU",
                                groupBankId = 13,
                                memo = null,
                                province = "-"
                            };
                            string setBankPayload = JsonConvert.SerializeObject(dataBank);
                            string setBankReponse = request.Post("https://www.cwin0066.com/api/1.0/bank/add", setBankPayload, "application/json; charset=utf-8").ToString();
                            JObject jsonObject1 = JObject.Parse(setBankReponse);
                            int codeBank = (int)jsonObject["Code"];
                            if (codeBank == 200)
                            {
                                string ketqua = sdt + "|" + username + "|" + pass + "|" + moneypass + "|" + name + "|" + bankaccount + "|" + setBankReponse + "\n";
                                this.Invoke(new Action(() =>
                                {
                                    File.AppendAllText($"TaiKhoan/Tai_Khoan_{fileName}.txt", ketqua);
                                }));
                            }
                            else
                            {
                                string ketqua = sdt + "|" + username + "|" + pass + "|" + moneypass + "|" + name + "|" + bankaccount + "|" + setBankReponse + "\n";
                                this.Invoke(new Action(() =>
                                {
                                    File.AppendAllText($"TaiKhoan/Tai_Khoan_{fileName}.txt", ketqua);
                                }));
                                //MessageBox.Show("Lỗi thêm bank");
                            }
                        }
                        else
                        {
                            string ketqua = sdt + "|" + username + "|" + pass + "|" + moneypass + "|" + name + "|" + bankaccount + "|" + setPassResponse + "\n";
                            this.Invoke(new Action(() =>
                            {
                                File.AppendAllText($"TaiKhoan/Tai_Khoan_{fileName}.txt", ketqua);
                            }));
                            //MessageBox.Show("Lỗi tạo tài khoản");
                        }
                        //MessageBox.Show("AccessToken: " + accessToken);
                    }
                    else
                    {
                        //MessageBox.Show("Không tìm thấy AccessToken trong JSON.");
                    }
                }
                else
                {
                    string ketqua = sdt + "|" + username + "|" + pass + "|" + moneypass + "|" + name + "|" + bankaccount + "|" + response + "\n";
                    this.Invoke(new Action(() =>
                    {
                        File.AppendAllText($"TaiKhoan/Tai_Khoan_{fileName}.txt", ketqua);
                    }));
                    //MessageBox.Show("Lỗi tạo tài khoản");
                }
            }
        }
        public void DangkyTaikhoanWin55(string sdt, string username, string pass, string moneypass, string name, string bankaccount, string captcha, string fileName)
        {

            token.AccessToken = null;
            token.RefreshToken = null;
            string proxy_text = textBox3.Text;
            string[] array_list = proxy_text.Split(':');
            if (array_list.Length != 2)
            {
                // Xử lý lỗi khi không đúng định dạng proxy
                return;
            }
            int proxyPort;
            if (!int.TryParse(array_list[1], out proxyPort))
            {
                // Xử lý lỗi khi không thể chuyển đổi cổng sang số
                return;
            }
            HttpProxyClient proxy = new HttpProxyClient(array_list[0], proxyPort);

            string[] user_agent = File.ReadAllLines("Mobile.txt");
            int userAgentIndex = rd.Next(0, user_agent.Length);
            using (HttpRequest request = new HttpRequest())
            {
                request.AddHeader("user-agent", user_agent[userAgentIndex]);
                request.Proxy = proxy;
                request.AddHeader("Accept", "application/json, text/plain, */*");
                request.AddHeader("Accept-Language", "en-US,en;q=0.9");
                request.AddHeader("Origin", "https://www.win5508.com");
                request.AddHeader("Referer", "https://www.win5508.com/");
                DataAccount account = new DataAccount()
                {
                    account = username,
                    bankAccount = null,
                    bankCity = null,
                    bankName = null,
                    bankProvince = null,
                    birthday = null,
                    checkCode = captcha,
                    checkCodeEncrypt = dataImage.value,
                    confirm_Password = pass,
                    countryCode = "84",
                    dealerAccount = null,
                    email = null,
                    groupBank = null,
                    idNumber = null,
                    isRequiredMoneyPassword = false,
                    mobile = null,
                    moneyPassword = null,
                    name = name,
                    parentAccount = "dx08",
                    password = pass,
                    qqAccount = null,
                    sex = null
                };
                string payload = JsonConvert.SerializeObject(account);
                string response = request.Post("https://www.55win55.com/api/1.0/member/register", payload, "application/json; charset=utf-8").ToString();
                string pattern_1 = "\"AccessToken\":\"([^\"]*)\"";
                Match match_1 = Regex.Match(response, pattern_1);
                JObject jsonObject = JObject.Parse(response);
                int code = (int)jsonObject["Code"];
                //MessageBox.Show(code.ToString());
                if (code == 200)
                {
                    if (match_1.Success)
                    {
                        // Lấy giá trị AccessToken từ match.Groups[1]
                        string accessToken = match_1.Groups[1].Value;
                        request.Proxy = proxy;
                        request.AddHeader("user-agent", user_agent[userAgentIndex]);
                        request.AddHeader("Authorization", "Bearer " + accessToken);
                        request.AddHeader("Accept", "application/json, text/plain, */*");
                        request.AddHeader("Accept-Language", "en-US,en;q=0.9");
                        request.AddHeader("Origin", "https://www.win5508.com");
                        request.AddHeader("Referer", "https://www.win5508.com/");
                        SetMoneyPass setMoneyPass = new SetMoneyPass()
                        {
                            isCreate = true,
                            newPassword = moneypass,
                            oldPassword = null,
                        };
                        string setPassPayload = JsonConvert.SerializeObject(setMoneyPass);
                        string setPassResponse = request.Post("https://www.55win55.com/api/0.0/Account/ChangeMoneyPasswordSubmit", setPassPayload, "application/json; charset=utf-8").ToString();
                        if (setPassResponse == "{\"IsSuccess\":true,\"ErrorMessage\":null}")
                        {
                            request.Proxy = proxy;
                            request.AddHeader("user-agent", user_agent[userAgentIndex]);
                            request.AddHeader("Authorization", "Bearer " + accessToken);
                            request.AddHeader("Accept", "application/json, text/plain, */*");
                            request.AddHeader("Accept-Language", "en-US,en;q=0.9");
                            request.AddHeader("Origin", "https://www.win5508.com");
                            request.AddHeader("Referer", "https://www.win5508.com/");
                            DataBank dataBank = new DataBank()
                            {
                                account = bankaccount,
                                city = "DIEN BIEN PHU",
                                groupBankId = 13,
                                memo = null,
                                province = "-"
                            };
                            string setBankPayload = JsonConvert.SerializeObject(dataBank);
                            string setBankReponse = request.Post("https://www.55win55.com/api/1.0/bank/add", setBankPayload, "application/json; charset=utf-8").ToString();
                            JObject jsonObject1 = JObject.Parse(setBankReponse);
                            int codeBank = (int)jsonObject["Code"];

                            if (codeBank == 200)
                            {

                                string ketqua = sdt + "|" + username + "|" + pass + "|" + moneypass + "|" + name + "|" + bankaccount + "|" + setBankReponse + "\n";
                                this.Invoke(new Action(() =>
                                {
                                    File.AppendAllText($"TaiKhoan/Tai_Khoan_{fileName}.txt", ketqua);
                                }));


                            }
                            else
                            {
                                string ketqua = sdt + "|" + username + "|" + pass + "|" + moneypass + "|" + name + "|" + bankaccount + "|" + setBankReponse + "\n";
                                this.Invoke(new Action(() =>
                                {
                                    File.AppendAllText($"TaiKhoan/Tai_Khoan_{fileName}.txt", ketqua);
                                }));
                                //MessageBox.Show("Lỗi thêm bank");
                            }
                        }
                        else
                        {
                            string ketqua = sdt + "|" + username + "|" + pass + "|" + moneypass + "|" + name + "|" + bankaccount + "|" + setPassResponse + "\n";
                            this.Invoke(new Action(() =>
                            {
                                File.AppendAllText($"TaiKhoan/Tai_Khoan_{fileName}.txt", ketqua);
                            }));
                            //MessageBox.Show("Lỗi tạo tài khoản");
                        }
                        //MessageBox.Show("AccessToken: " + accessToken);
                    }
                    else
                    {
                        //MessageBox.Show("Không tìm thấy AccessToken trong JSON.");
                    }
                }
                else
                {
                    string ketqua = sdt + "|" + username + "|" + pass + "|" + moneypass + "|" + name + "|" + bankaccount + "|" + response + "\n";
                    this.Invoke(new Action(() =>
                    {
                        File.AppendAllText($"TaiKhoan/Tai_Khoan_{fileName}.txt", ketqua);
                    }));
                    //MessageBox.Show("Lỗi tạo tài khoản");
                }

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            dataImage = null;
            string proxy_text = textBox3.Text;
            string[] array_list = proxy_text.Split(':');
            if (array_list.Length != 2)
            {
                // Xử lý lỗi khi không đúng định dạng proxy
                return;
            }
            int proxyPort;
            if (!int.TryParse(array_list[1], out proxyPort))
            {
                // Xử lý lỗi khi không thể chuyển đổi cổng sang số
                return;
            }
            HttpProxyClient proxy = new HttpProxyClient(array_list[0], proxyPort);
            string[] user_agent = File.ReadAllLines("Mobile.txt");
            int userAgentIndex = rd.Next(0, user_agent.Length);
            using (HttpRequest request = new HttpRequest())
            {
                try
                {
                    request.AddHeader("user-agent", user_agent[userAgentIndex]);
                    request.Proxy = proxy;
                    string response = request.Post("https://www.55win55.com/api/0.0/Home/GetCaptchaForRegister", " ", "application/json; charset=utf-8").ToString();
                    dataImage = JsonConvert.DeserializeObject<DataImage>(response);
                    string base64String = dataImage.image;
                    byte[] imageBytes = Convert.FromBase64String(base64String);
                    using (MemoryStream memoryStream = new MemoryStream(imageBytes))
                    {
                        Image image = Image.FromStream(memoryStream);
                        pictureBox2.Image = image;
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Kiểm tra lại Proxy");
                }

            }
            File.WriteAllText("Proxy.txt", textBox3.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sendOtpWin55(SdtTextBox.Text);
        }
        public void sendOtpWin55(string phone_number)
        {
            int code;
            if (!Int32.TryParse(textBox2.Text, out code))
            {
                // Xử lý lỗi khi textBox3 không chứa một giá trị số nguyên hợp lệ
                MessageBox.Show("Captcha không xác định");
                return;
            }

            string[] proxy_array = textBox3.Text.Split(':');
            if (proxy_array.Length != 2)
            {
                // Xử lý lỗi khi textBox2 không chứa chuỗi "host:port" hợp lệ
                MessageBox.Show("Proxy không xác định");
                return;
            }

            string[] user_agent;
            try
            {
                user_agent = File.ReadAllLines("Mobile.txt");
            }
            catch (FileNotFoundException)
            {
                // Xử lý lỗi khi tệp "ua.txt" không tồn tại
                MessageBox.Show("Thiếu file ua.txt");
                return;
            }

            SendMobileRequest sendMobileRequest = new SendMobileRequest();
            HttpRequest httpRequest = new HttpRequest();
            HttpProxyClient proxy = new HttpProxyClient(proxy_array[0], Int32.Parse(proxy_array[1]));
            httpRequest.Proxy = proxy;
            Random random_ua = new Random();
            int index = random_ua.Next(user_agent.Length);
            string ua = user_agent[index];
            httpRequest.UserAgent = ua;
            httpRequest.AddHeader("Accept", "application/json, text/plain, *");
            httpRequest.AddHeader("Accept-Language", "en-US,en;q=0.9");
            httpRequest.AddHeader("Origin", "https://www.win5508.com");
            httpRequest.AddHeader("Referer", "https://www.win5508.com/");
            string response = httpRequest.Get("https://www.137722.cc/api/send?mobile=" + phone_number + "&catcph=" + code).ToString();

            try
            {
                sendMobileRequest = JsonConvert.DeserializeObject<SendMobileRequest>(response);
            }
            catch (JsonException)
            {
                // Xử lý lỗi khi dữ liệu JSON không được phân tích đúng
                MessageBox.Show("Error parsing JSON response");
                return;
            }

            // Hiển thị thông báo
            MessageBox.Show(sendMobileRequest.message);

        }





        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            string[] proxy_array = textBox4.Lines;
            string url = textBox5.Text;
            for (int i = 0; i < proxy_array.Length; i++)
            {
                moChrome(url, proxy_array[i]);
            }

        }

        public void moChrome(string url, string proxyRoot)
        {
            try
            {
                string[] user_agent = File.ReadAllLines("Desktop.txt");
                Random random_ua = new Random();
                int uaIndex = random_ua.Next(0, user_agent.Length);
                string ua = user_agent[uaIndex];
                ChromeOptions options = new ChromeOptions();
                int screenWidth = Screen.PrimaryScreen.Bounds.Width; // Độ rộng màn hình
                int screenHeight = Screen.PrimaryScreen.Bounds.Height; // Chiều cao màn hình            
                string[] proxy_array = proxyRoot.Split(':');
                string proxyAddress = proxy_array[0];
                string proxyPort = proxy_array[1];
                options.AddArgument($"--proxy-server={proxyAddress}:{proxyPort}");
                options.AddArgument($"--user-agent={ua}");
                options.AddArgument("disable-infobars");
                IWebDriver driverChrome = new ChromeDriver(options);
                driverChrome.Manage().Window.Size = new System.Drawing.Size(screenWidth / 2, (screenHeight - 30) / 2);
                driverChrome.Manage().Window.Position = new Point(0, 0);
                driverChrome.Navigate().GoToUrl(url);
                IJavaScriptExecutor js = (IJavaScriptExecutor)driverChrome;
                js.ExecuteScript("document.body.style.zoom='80%'");
                string currentUA = (string)js.ExecuteScript("return navigator.userAgent;");
                listChrome.Add(driverChrome);
                //MessageBox.Show(currentUA);
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi IP");
            }


        }

        private void button8_Click(object sender, EventArgs e)
        {
            foreach (IWebDriver driver in listChrome)
            {

                dongChrome(driver);

            }
        }
        public void dongChrome(IWebDriver driver)
        {
            driver.Quit();
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("Data.txt");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("Data.txt");
        }

        IronTesseract ocr = new IronTesseract();
        public void SendOtpWin55(string proxyInput)
        {
            string[] array_list = proxyInput.Split(':');
            HttpProxyClient proxy = new HttpProxyClient(array_list[0], Int32.Parse(array_list[1]));
            string[] user_agent = File.ReadAllLines("Mobile.txt");
            int userAgentIndex = rd.Next(0, user_agent.Length);
            using (HttpRequest request = new HttpRequest())
            {
                request.AddHeader("user-agent", user_agent[userAgentIndex]);
                request.Proxy = proxy;
                string response = request.Post("https://www.55win55.com/api/0.0/Home/GetCaptchaForRegister", " ", "application/json; charset=utf-8").ToString();
                dataImage = JsonConvert.DeserializeObject<DataImage>(response);
                string base64String = dataImage.image;
                byte[] imageBytes = Convert.FromBase64String(base64String);
                using (MemoryStream memoryStream = new MemoryStream(imageBytes))
                {
                    Image image = Image.FromStream(memoryStream);
                    image.Save("anh.jpg");
                    var result = ocr.Read("anh.jpg");
                    pictureBox3.Image = image;
                    string kq = result.Text;
                    char[] chars = kq.ToCharArray();
                    this.Invoke(new Action(() =>
                    {
                        textBox6.Text = kq;
                    }));

                    File.WriteAllText("Proxy.txt", textBox3.Text);
                }
            }
        }


        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            SendOtpWin55(textBox3.Text);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string[] array_list = richTextBox1.Text.Split('|');
                if (string.IsNullOrWhiteSpace(richTextBox1.Text))
                {
                    Process currentProcess = Process.GetCurrentProcess();
                    ProcessThreadCollection threadCollection = currentProcess.Threads;
                    foreach (ProcessThread thread in threadCollection)
                    {
                        thread.Dispose();
                    }
                    textBox12.Text = "";
                    textBox6.Text = "Chờ giải Captcha...";
                    textBox7.Text = "";
                    textBox8.Text = "";
                    textBox9.Text = "";
                    textBox10.Text = "";
                    textBox11.Text = "";
                }
                else if (array_list.Length >= 5)
                {
                    Thread t = new Thread(() =>
                    {
                        this.Invoke(new Action(() =>
                        {
                            textBox6.Text = "Đang giải captcha...";
                            textBox12.Text = array_list[0];
                            textBox7.Text = array_list[1];
                            textBox8.Text = array_list[2];
                            textBox9.Text = array_list[3];
                            textBox10.Text = array_list[4];
                            textBox11.Text = array_list[5];
                        }));
                        SendOtpWin55(textBox3.Text);
                    });
                    t.Start();
                    t.IsBackground = false;
                }
            }
            catch (Exception)
            {
                // Xử lý ngoại lệ (nếu cần)
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            char[] chars = textBox6.Text.ToCharArray();
            if (chars.Length != 4)
            {
                MessageBox.Show("Sai Captcha, vui lòng kiểm tra lại");
            }
            else
            {
                //MessageBox.Show("Captcha đúng");
            }

        }
        public void RegWin55()
        {
            string sdt = textBox12.Text;
            string captcha = textBox6.Text;
            string username = textBox7.Text;
            string pass = textBox8.Text;
            string moneypass = textBox9.Text;
            string name = textBox10.Text;
            string bankaccount = textBox11.Text;
            DateTime time = DateTime.Now;
            string crtime = time.ToString("yy-MM-dd-hh-mm-ss");
            token.AccessToken = null;
            token.RefreshToken = null;
            string proxy_text = textBox3.Text;
            string[] array_list = proxy_text.Split(':');
            if (array_list.Length != 2)
            {
                // Xử lý lỗi khi không đúng định dạng proxy
                return;
            }
            int proxyPort;
            if (!int.TryParse(array_list[1], out proxyPort))
            {
                // Xử lý lỗi khi không thể chuyển đổi cổng sang số
                return;
            }
            HttpProxyClient proxy = new HttpProxyClient(array_list[0], proxyPort);
            string[] user_agent = File.ReadAllLines("Mobile.txt");
            int userAgentIndex = rd.Next(0, user_agent.Length);
            using (HttpRequest request = new HttpRequest())
            {
                request.AddHeader("user-agent", user_agent[userAgentIndex]);
                request.Proxy = proxy;
                request.AddHeader("Accept", "application/json, text/plain, */*");
                request.AddHeader("Accept-Language", "en-US,en;q=0.9");
                request.AddHeader("Origin", "https://www.win5508.com");
                request.AddHeader("Referer", "https://www.win5508.com/");
                DataAccount account = new DataAccount()
                {
                    account = username,
                    bankAccount = null,
                    bankCity = null,
                    bankName = null,
                    bankProvince = null,
                    birthday = null,
                    checkCode = captcha,
                    checkCodeEncrypt = dataImage.value,
                    confirm_Password = pass,
                    countryCode = "84",
                    dealerAccount = null,
                    email = null,
                    groupBank = null,
                    idNumber = null,
                    isRequiredMoneyPassword = false,
                    mobile = null,
                    moneyPassword = null,
                    name = name,
                    parentAccount = "dx08",
                    password = pass,
                    qqAccount = null,
                    sex = null
                };
                string payload = JsonConvert.SerializeObject(account);
                string response = request.Post("https://www.55win55.com/api/1.0/member/register", payload, "application/json; charset=utf-8").ToString();
                string pattern_1 = "\"AccessToken\":\"([^\"]*)\"";
                Match match_1 = Regex.Match(response, pattern_1);
                JObject jsonObject = JObject.Parse(response);
                int code = (int)jsonObject["Code"];
                //MessageBox.Show(code.ToString());
                if (code == 200)
                {
                    if (match_1.Success)
                    {
                        // Lấy giá trị AccessToken từ match.Groups[1]
                        string accessToken = match_1.Groups[1].Value;
                        request.Proxy = proxy;
                        request.AddHeader("user-agent", user_agent[userAgentIndex]);
                        request.AddHeader("Authorization", "Bearer " + accessToken);
                        request.AddHeader("Accept", "application/json, text/plain, */*");
                        request.AddHeader("Accept-Language", "en-US,en;q=0.9");
                        request.AddHeader("Origin", "https://www.win5508.com");
                        request.AddHeader("Referer", "https://www.win5508.com/");
                        SetMoneyPass setMoneyPass = new SetMoneyPass()
                        {
                            isCreate = true,
                            newPassword = moneypass,
                            oldPassword = null,
                        };
                        string setPassPayload = JsonConvert.SerializeObject(setMoneyPass);
                        string setPassResponse = request.Post("https://www.55win55.com/api/0.0/Account/ChangeMoneyPasswordSubmit", setPassPayload, "application/json; charset=utf-8").ToString();
                        if (setPassResponse == "{\"IsSuccess\":true,\"ErrorMessage\":null}")
                        {
                            request.Proxy = proxy;
                            request.AddHeader("user-agent", user_agent[userAgentIndex]);
                            request.AddHeader("Authorization", "Bearer " + accessToken);
                            request.AddHeader("Accept", "application/json, text/plain, */*");
                            request.AddHeader("Accept-Language", "en-US,en;q=0.9");
                            request.AddHeader("Origin", "https://www.win5508.com");
                            request.AddHeader("Referer", "https://www.win5508.com/");
                            DataBank dataBank = new DataBank()
                            {
                                account = bankaccount,
                                city = "DIEN BIEN PHU",
                                groupBankId = 13,
                                memo = null,
                                province = "-"
                            };
                            string setBankPayload = JsonConvert.SerializeObject(dataBank);
                            string setBankReponse = request.Post("https://www.55win55.com/api/1.0/bank/add", setBankPayload, "application/json; charset=utf-8").ToString();
                            JObject jsonObject1 = JObject.Parse(setBankReponse);
                            int codeBank = (int)jsonObject["Code"];

                            if (codeBank == 200)
                            {

                                string ketqua = crtime + "|" + sdt + "|" + username + "|" + pass + "|" + moneypass + "|" + name + "|" + bankaccount + "|" + setBankReponse + "\n";
                                this.Invoke(new Action(() =>
                                {
                                    File.AppendAllText($"TaiKhoan/Tai_Khoan.txt", ketqua);
                                    PushData(sdt, username, pass, moneypass, name, bankaccount, setBankReponse);
                                }));


                            }
                            else
                            {
                                string ketqua = crtime + "|" + sdt + "|" + username + "|" + pass + "|" + moneypass + "|" + name + "|" + bankaccount + "|" + setBankReponse + "\n";
                                this.Invoke(new Action(() =>
                                {
                                    File.AppendAllText($"TaiKhoan/Tai_Khoan.txt", ketqua);
                                    PushData(sdt, username, pass, moneypass, name, bankaccount, setBankReponse);
                                }));
                                //MessageBox.Show("Lỗi thêm bank");
                            }
                        }
                        else
                        {
                            string ketqua = crtime + "|" + sdt + "|" + username + "|" + pass + "|" + moneypass + "|" + name + "|" + bankaccount + "|" + setPassResponse + "\n";
                            this.Invoke(new Action(() =>
                            {
                                File.AppendAllText($"TaiKhoan/Tai_Khoan.txt", ketqua);
                                PushData(sdt, username, pass, moneypass, name, bankaccount, setPassResponse);
                            }));
                            //MessageBox.Show("Lỗi tạo tài khoản");
                        }
                        //MessageBox.Show("AccessToken: " + accessToken);
                    }
                    else
                    {
                        //MessageBox.Show("Không tìm thấy AccessToken trong JSON.");
                    }
                }
                else
                {
                    string ketqua = crtime + "|" + sdt + "|" + username + "|" + pass + "|" + moneypass + "|" + name + "|" + bankaccount + "|" + response + "\n";
                    this.Invoke(new Action(() =>
                    {
                        File.AppendAllText($"TaiKhoan/Tai_Khoan.txt", ketqua);
                        PushData(sdt, username, pass, moneypass, name, bankaccount, response);
                    }));
                    //MessageBox.Show("Lỗi tạo tài khoản");
                }

            }
        }
        public void PushData(string sdt, string username, string pass, string moneypass, string name, string bankaccount, string status)
        {
            try
            {
                string[] Scopes = { SheetsService.Scope.Spreadsheets };
                var service = new SheetsService(new BaseClientService.Initializer() { HttpClientInitializer = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets { ClientId = "581209738182-df30q1tuof83ialjdrnd58akudbkudb6.apps.googleusercontent.com", ClientSecret = "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" }, Scopes, "user", CancellationToken.None, new FileDataStore("MyAppsToken")).Result, ApplicationName = "RegApp", });
                SpreadsheetsResource.ValuesResource.GetRequest getRequest = service.Spreadsheets.Values.Get("1Fv1F72ONAyjiZaVk_ORBu5iTr0WE4aWggr8rA3FBz4w", "TaiKhoan!A:C");
                ServicePointManager.ServerCertificateValidationCallback = delegate (object sender2, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                ValueRange getResponse = getRequest.Execute();
                ///SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum valueInputOption = (SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum)0;  // TODO: Update placeholder value.
                IList<IList<Object>> values = getResponse.Values;
                var Range = $"{"TaiKhoan"}!A" + (values.Count + 1) + ":F" + (values.Count + 1);
                var ValueRange = new ValueRange();
                ValueRange.Values = new List<IList<object>> { new List<object>() { textBox12.Text, textBox7.Text, textBox8.Text, textBox9.Text, textBox10.Text, textBox11.Text } };
                SpreadsheetsResource.ValuesResource.UpdateRequest updateRequest = service.Spreadsheets.Values.Update(ValueRange, "1Fv1F72ONAyjiZaVk_ORBu5iTr0WE4aWggr8rA3FBz4w", Range);
                updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
                Google.Apis.Sheets.v4.Data.UpdateValuesResponse updateResponse = updateRequest.Execute();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {

        }
    }
}

