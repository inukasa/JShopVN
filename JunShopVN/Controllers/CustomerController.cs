using JunShopVN.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JunShopVN.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            using (JShopVNEntities db = new JShopVNEntities())
            {
                return View(db.Games.ToList());
            }

        }
        public ActionResult PackageGame(int id)
        {
            using (JShopVNEntities db = new JShopVNEntities())
            {
                return View(db.Packages.Where(x => x.gameid == id).ToList());
            }
        }
        [Authorize]
        public ActionResult AddCart(int id)
        {
            if(Session["username"] != null)
            {
                using (JShopVNEntities db = new JShopVNEntities())
                {
                    string user = Session["username"].ToString();
                    Cart ca = new Cart();
                    List<Cart> lc = db.Carts.ToList();
                    if(lc.Count == 0)
                    {
                        ca.id = 1;
                        ca.packid = id;
                        ca.quantity = 1;
                        ca.userid = db.Accounts.Where(x => x.username.Equals(user)).FirstOrDefault().id;
                        ca.totalprice = db.Packages.Where(x => x.id == id).FirstOrDefault().price;
                        db.Carts.Add(ca);
                        db.SaveChanges();
                        TempData["msg"] = "<script>alert('Thêm vào giỏ hàng thành công');</script>";
                        return RedirectToAction("PackageGame", "Customer", new { id = db.Games.Where(x => x.id == db.Packages.Where(y => y.id == ca.packid).FirstOrDefault().gameid).FirstOrDefault().id });
                    }
                    else
                    {
                        List<Cart> lc2 = db.Carts.Where(x => x.userid == db.Accounts.Where(y => y.username.Equals(user)).FirstOrDefault().id).ToList();
                        bool flag = false;
                        foreach(Cart c in lc2)
                        {
                            if(c.packid == id)
                            {
                                flag = true;
                            }
                        }
                        if(flag == true)
                        {
                            Cart ca2 = db.Carts.Where(x => x.packid == id && x.userid == db.Accounts.Where(y => y.username.Equals(user)).FirstOrDefault().id).FirstOrDefault();
                            ca2.quantity += 1;
                            ca2.totalprice += db.Packages.Where(x => x.id == id).FirstOrDefault().price;
                            var local = db.Set<Cart>().Local.FirstOrDefault(f => f.id == ca2.id);
                            if (local != null)
                            {
                                db.Entry(local).State = EntityState.Detached;
                            }
                            db.Entry(ca2).State = EntityState.Modified;
                            db.SaveChanges();
                            TempData["msg"] = "<script>alert('Thêm vào giỏ hàng thành công');</script>";
                            return RedirectToAction("PackageGame", "Customer", new { id = db.Games.Where(x => x.id == db.Packages.Where(y => y.id == ca2.packid).FirstOrDefault().gameid).FirstOrDefault().id});
                        }
                        else
                        {
                            ca.id = lc[lc.Count-1].id +1;
                            ca.packid = id;
                            ca.quantity = 1;
                            ca.userid = db.Accounts.Where(x => x.username.Equals(user)).FirstOrDefault().id;
                            ca.totalprice = db.Packages.Where(x => x.id == id).FirstOrDefault().price;
                            db.Carts.Add(ca);
                            db.SaveChanges();
                            TempData["msg"] = "<script>alert('Thêm vào giỏ hàng thành công');</script>";
                            return RedirectToAction("PackageGame", "Customer", new { id = db.Games.Where(x => x.id == db.Packages.Where(y => y.id == ca.packid).FirstOrDefault().gameid).FirstOrDefault().id });
                        }
                    }
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        [Authorize]
        public ActionResult ListCart(int id)
        {
            using (JShopVNEntities db = new JShopVNEntities())
            {
                return View(db.Carts.Where(x => x.userid == id).ToList());
            }
        }

        [Authorize]
        public ActionResult Decrease(int id, Cart cart)
        {
            using (JShopVNEntities db = new JShopVNEntities())
            {
                if (db.Carts.Where(x => x.id == id).FirstOrDefault().quantity == 1)
                {
                    TempData["ErrorMes"] = "<script>alert('Số lượng mặt hàng không thể bé hơn 1');</script>";
                    return RedirectToAction("ListCart", "Customer", new { id = db.Carts.Where(x => x.id == id).FirstOrDefault().userid });
                }
                else 
                {
                    cart = db.Carts.Where(x => x.id == id).FirstOrDefault();
                    cart.quantity -= 1;
                    cart.totalprice -= db.Packages.Where(x => x.id == cart.packid).FirstOrDefault().price;
                    var local = db.Set<Cart>()
                             .Local
                             .FirstOrDefault(f => f.id == id);
                    if (local != null)
                    {
                        db.Entry(local).State = EntityState.Detached;
                    }
                    db.Entry(cart).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ListCart", "Customer", new { id = cart.userid });
                }
            }
        }
        [Authorize]
        public ActionResult Increase(int id, Cart cart)
        {
            using (JShopVNEntities db = new JShopVNEntities())
            {
                    cart = db.Carts.Where(x => x.id == id).FirstOrDefault();
                    cart.quantity += 1;
                    cart.totalprice += db.Packages.Where(x => x.id == cart.packid).FirstOrDefault().price;
                    var local = db.Set<Cart>()
                             .Local
                             .FirstOrDefault(f => f.id == id);
                    if (local != null)
                    {
                        db.Entry(local).State = EntityState.Detached;
                    }
                    db.Entry(cart).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ListCart", "Customer", new { id = cart.userid });
            }
        }
        [Authorize]
        public ActionResult Delete(int id, Cart cart)
        {
            using (JShopVNEntities db = new JShopVNEntities())
            {

                cart = db.Carts.Where(x => x.id == id).FirstOrDefault();
                var uid = cart.userid;
                db.Carts.Remove(cart);
                db.SaveChanges();
                return RedirectToAction("ListCart", "Customer", new { id = uid });
            }
        }
        [Authorize]
        public ActionResult Checkout()
        {
            decimal totalPrice = 0;
            string user = Session["username"].ToString();
            using (JShopVNEntities db = new JShopVNEntities())
            {
                int userid = db.Users.Where(x => x.accid == db.Accounts.Where(y => y.username.Equals(user)).FirstOrDefault().id).FirstOrDefault().id;
                List<Cart> listCart = db.Carts.Where(x => x.userid == userid).ToList();
                foreach(Cart c in listCart)
                {
                    totalPrice += c.totalprice;
                }
                if(totalPrice > db.Users.Where(x => x.id == userid).FirstOrDefault().balance)
                {
                    TempData["ErrorMes"] = "<script>alert('Tài khoản của bạn không đủ để xác nhận đơn hàng');</script>";
                    return RedirectToAction("ListCart", "Customer", new { id = userid });
                }
                else
                {
                    Order od = new Order();
                    List<Order> lo = db.Orders.ToList();
                    List<OrderDetail> lod = db.OrderDetails.ToList();
                    if(lo.Count == 0)
                    {
                        od.id = 1;
                        od.status = "Đang chờ xử lí";
                        od.userid = userid;
                        od.totalprice = totalPrice;
                        od.datecreated = DateTime.Now;
                        db.Orders.Add(od);
                        db.SaveChanges();
                        User us = db.Users.Where(x => x.id == userid).FirstOrDefault();
                        us.balance -= totalPrice;
                        var local = db.Set<User>()
                            .Local
                            .FirstOrDefault(f => f.id == us.id);
                        if (local != null)
                        {
                            db.Entry(local).State = EntityState.Detached;
                        }
                        db.Entry(us).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                    else
                    {
                        od.id = lo[lo.Count - 1].id + 1;
                        od.status = "Đang chờ xử lí";
                        od.userid = userid;
                        od.totalprice = totalPrice;
                        od.datecreated = DateTime.Now;
                        db.Orders.Add(od);
                        db.SaveChanges();
                        User us = db.Users.Where(x => x.id == userid).FirstOrDefault();
                        us.balance -= totalPrice;
                        var local = db.Set<User>()
                            .Local
                            .FirstOrDefault(f => f.id == us.id);
                        if (local != null)
                        {
                            db.Entry(local).State = EntityState.Detached;
                        }
                        db.Entry(us).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    if(lod.Count == 0)
                    {
                        int odtid = 1;
                        foreach(Cart c in listCart)
                        {
                            OrderDetail odt = new OrderDetail();
                            odt.id = odtid;
                            odt.orderid = od.id;
                            odt.packid = c.packid;
                            odt.quantity = c.quantity;
                            odt.price = c.totalprice;
                            db.OrderDetails.Add(odt);
                            db.SaveChanges();
                            odtid += 1;
                            
                        }
                        return RedirectToAction("ListOrder", "Customer", new { id = userid });
                    }
                    else
                    {
                        int odtid = lod[lod.Count - 1].id + 1;
                        foreach (Cart c in listCart)
                        {
                            OrderDetail odt = new OrderDetail();
                            odt.id = odtid;
                            odt.orderid = od.id;
                            odt.packid = c.packid;
                            odt.quantity = c.quantity;
                            odt.price = c.totalprice;
                            db.OrderDetails.Add(odt);
                            db.SaveChanges();
                            odtid += 1; 
                        }
                        return RedirectToAction("ListOrder", "Customer", new { id = userid });
                    }
                }
            }
        }
        [Authorize]
        public ActionResult ListOrder(int id)
        {
            using (JShopVNEntities db = new JShopVNEntities())
            {
                return View(db.Orders.Where(x => x.userid == id).ToList());
            }
        }
        [Authorize]
        public ActionResult CancelOrder(int id)
        {
            using (JShopVNEntities db = new JShopVNEntities())
            {
                Order od = db.Orders.Where(x => x.id == id).FirstOrDefault();
                od.status = "Đã hủy";
                var local = db.Set<Order>()
                             .Local
                             .FirstOrDefault(f => f.id == id);
                if (local != null)
                {
                    db.Entry(local).State = EntityState.Detached;
                }
                db.Entry(od).State = EntityState.Modified;
                db.SaveChanges();
                User us = db.Users.Where(x => x.id == od.userid).FirstOrDefault();
                us.balance += od.totalprice;
                var local2 = db.Set<User>()
                    .Local
                    .FirstOrDefault(f => f.id == us.id);
                if (local2 != null)
                {
                    db.Entry(local2).State = EntityState.Detached;
                }
                db.Entry(us).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListOrder", "Customer", new { id = od.userid });
            }
        }
    }
}