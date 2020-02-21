using JunShopVN.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace JunShopVN.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserLoginModels user)
        {
            if (ModelState.IsValid)
            {
                if(IsValid(user) == 2)
                {
                    FormsAuthentication.SetAuthCookie(user.username, false);
                    Session["username"] = user.username;
                    return RedirectToAction("Index", "Customer");
                }
                else
                {
                    ViewBag.ErrorMessege = "<script>alert('Sai tài khoản hoặc mật khẩu');</script>";
                    return View(user);
                }
            }
            else
            {
                return View(user);
            }
        }
        public ActionResult LoginA()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginA(UserLoginModels user, string returnURL)
        {
            if (ModelState.IsValid)
            {
                if (IsValid(user) == 2)
                {
                    FormsAuthentication.SetAuthCookie(user.username, false);
                    Session["username"] = user.username;
                    return RedirectToAction("Index", "Customer");
                }
                else
                {
                    ViewBag.ErrorMessege = "<script>alert('Sai tài khoản hoặc mật khẩu');</script>";
                    return View(user);
                }
            }
            else
            {
                return View(user);
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["user"] = null;
            Session["username"] = null;
            return Redirect("/Login/Login");
        }
        private int IsValid(UserLoginModels user)
        {
            int flag = 0;
            using (JShopVNEntities db1 = new JShopVNEntities())
            {
                var uLogin = new Account();
                    if (db1.Accounts
                    .Where(b => b.username.Equals(user.username) && b.password.Equals(user.password))
                    .FirstOrDefault() != null)
                    {
                        uLogin = db1.Accounts.Where(b => b.username.Equals(user.username) && b.password.Equals(user.password)).FirstOrDefault();
                    }
               
                if (uLogin != null)
                {
                    if (uLogin.roleid == 1)
                    {
                        flag = 1;
                    }
                    else if (uLogin.roleid == 2)
                    {
                        flag = 2;
                    }

                }
                else
                {
                    flag = 0;
                }
            }
            return flag;
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterUserModel user)
        {
            if (ModelState.IsValid)
            {
                using (JShopVNEntities db = new JShopVNEntities())
                {
                    bool flag = true;
                    Account acc = new Account();
                    User us = new User();
                    List<Account> la = db.Accounts.ToList();
                    List<User> lu = db.Users.ToList();
                    foreach(Account a in la)
                    {
                        if (user.username.ToLower().Equals(a.username.ToLower()))
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        if (la.Count == 0)
                        {
                            acc.id = 1;
                            acc.username = user.username;
                            acc.password = user.password;
                            acc.roleid = 2;
                            db.Accounts.Add(acc);
                            db.SaveChanges();
                            if (lu.Count == 0)
                            {
                                us.id = 1;
                                us.name = user.name;
                                us.phone = user.phone;
                                us.email = user.email;
                                us.facebook = user.facebook;
                                us.balance = 0;
                                us.accid = acc.id;
                                db.Users.Add(us);
                                db.SaveChanges();
                            }
                            else
                            {
                                us.id = lu[lu.Count - 1].id + 1;
                                us.name = user.name;
                                us.phone = user.phone;
                                us.email = user.email;
                                us.facebook = user.facebook;
                                us.balance = 0;
                                us.accid = acc.id;
                                db.Users.Add(us);
                                db.SaveChanges();
                            }
                            TempData["msg"] = "<script>alert('Tạo tài khoản thành công');</script>";
                            return RedirectToAction("Login", "Login");
                        }
                        else
                        {
                            acc.id = la[la.Count - 1].id + 1;
                            acc.username = user.username;
                            acc.password = user.password;
                            acc.roleid = 2;
                            db.Accounts.Add(acc);
                            db.SaveChanges();
                            if (lu.Count == 0)
                            {
                                us.id = 1;
                                us.name = user.name;
                                us.phone = user.phone;
                                us.email = user.email;
                                us.facebook = user.facebook;
                                us.balance = 0;
                                us.accid = acc.id;
                                db.Users.Add(us);
                                db.SaveChanges();
                            }
                            else
                            {
                                us.id = lu[lu.Count - 1].id + 1;
                                us.name = user.name;
                                us.phone = user.phone;
                                us.email = user.email;
                                us.facebook = user.facebook;
                                us.balance = 0;
                                us.accid = acc.id;
                                db.Users.Add(us);
                                db.SaveChanges();
                            }
                            TempData["msg"] = "<script>alert('Tạo tài khoản thành công');</script>";
                            return RedirectToAction("Login", "Login");
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessege = "<script>alert('Tài khoản được tạo đã có, tạo lại tài khoản khác');</script>";
                        return View();
                    }
                    
                }
            }
            else
            {
                return View();
            }
        }
        [Authorize]
        public ActionResult viewProfile()
        {
            var username = Session["username"].ToString();
            using (JShopVNEntities db = new JShopVNEntities())
            {
                return View(db.Users.Where(x => x.accid == db.Accounts.Where(y => y.username.Equals(username)).FirstOrDefault().id).FirstOrDefault());
            }
        }
        [Authorize]
        public ActionResult EditProfile()
        {
            var username = Session["username"].ToString();
            using (JShopVNEntities db = new JShopVNEntities())
            {
                return View(db.Users.Where(x => x.accid == db.Accounts.Where(y => y.username.Equals(username)).FirstOrDefault().id).FirstOrDefault());
            }
        }
        [Authorize]
        [HttpPost]
        public ActionResult EditProfile(int id, User us)
        {
            using (JShopVNEntities db = new JShopVNEntities())
            {
                var local = db.Set<User>().Local.FirstOrDefault(f => f.id == id);
                if (local != null)
                {
                    db.Entry(local).State = EntityState.Detached;
                }
                db.Entry(us).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "<script>alert('Cập nhật thông tin người dùng thành công');</script>";
                return RedirectToAction("viewProfile");
            }
        }
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(Account acc)
        {
            try
            {
                if (Session["username"] != null)
                {
                    using (JShopVNEntities db = new JShopVNEntities())
                    {
                        var username = Session["username"].ToString();
                        var opass = Request["oldpass"].ToString();
                        var npass = Request["newpass"].ToString();
                        var cnpass = Request["confirmnewpass"].ToString();
                        acc = db.Accounts.Where(x => x.username.Equals(username)).FirstOrDefault();
                        if (acc.password.Equals(opass))
                        {
                            if (npass.Equals(cnpass))
                            {
                                acc.password = npass;
                                var local = db.Set<Account>()
                        .Local
                        .FirstOrDefault(f => f.username.Equals(username));
                                if (local != null)
                                {
                                    db.Entry(local).State = EntityState.Detached;
                                }
                                db.Entry(acc).State = EntityState.Modified;
                                db.SaveChanges();
                                return RedirectToAction("ViewProfile");
                            }
                            else
                            {
                                ViewBag.ErrorMessege = "<script>alert('Confirm không trùng với password mới');</script>";
                                return View();
                            }
                        }
                        else
                        {
                            ViewBag.ErrorMessege = "<script>alert('Password cũ sai');</script>";
                            return View();
                        }

                    }
                }
                else
                {
                    return RedirectToAction("LoginA", "Login");
                }
            }
            catch
            {
                ViewBag.ErrorMessege = "<script>alert('Exception');</script>";
                return View();
            }
        }
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(EmailModel model)
        {
            string senderEmail = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"].ToString();
            string senderPass = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"].ToString();
            using (JShopVNEntities db = new JShopVNEntities())
            {
                if (db.Accounts.Where(x => x.username.Equals(model.Username)).FirstOrDefault() != null)
                {
                    if (db.Users.Where(x => x.accid == (db.Accounts.Where(y => y.username.Equals(model.Username)).FirstOrDefault().id)).FirstOrDefault().email.Equals(model.Email))
                    {
                        using (MailMessage mm = new MailMessage(senderEmail, model.Email))
                        {
                            mm.Subject = "Reset password";
                            Random rnd = new Random();

                            string pass = "";
                            for (int x = 0; x < 6; x += 1)
                            {
                                char randomChar = (char)rnd.Next('a', 'z');
                                pass += randomChar;
                            }
                            Account us = db.Accounts.Where(x => x.username.Equals(model.Username)).FirstOrDefault();
                            us.password = pass;
                            var local = db.Set<Account>()
                            .Local
                            .FirstOrDefault(f => f.username.Equals(model.Username));
                            if (local != null)
                            {
                                db.Entry(local).State = EntityState.Detached;
                            }
                            db.Entry(us).State = EntityState.Modified;
                            db.SaveChanges();
                            mm.Body = "reset lại password cho nè:  " + pass;
                            mm.IsBodyHtml = false;
                            using (SmtpClient smtp = new SmtpClient())
                            {
                                smtp.Host = "smtp.gmail.com";
                                smtp.EnableSsl = true;
                                NetworkCredential NetworkCred = new NetworkCredential(senderEmail, senderPass);
                                smtp.UseDefaultCredentials = true;
                                smtp.Credentials = NetworkCred;
                                smtp.Port = 587;
                                smtp.Send(mm);
                                ViewBag.Message = "Email sent.";
                            }
                        }

                        return View();
                    }
                    else
                    {
                        ViewBag.ErrorMessege = "<script>alert('Email đăng ký không trùng khớp');</script>";
                        return View();
                    }
                }
                else
                {
                    ViewBag.ErrorMessege = "<script>alert('Không có user');</script>";
                    return View();
                }

            }

        }
    }
}