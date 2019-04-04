using M3Test_2.Data;
using M3Test_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace M3Test_2.Controllers
{
    public class WorkController : Controller
    {

        private readonly DavidEntities _entities;
        public WorkController()
        {
            _entities = new DavidEntities();
        }


        // GET: Work
        public ActionResult GetDetails()
        {
            GetAllUserViewModel model = new GetAllUserViewModel();

            List<userData> list = new List<userData>();

            var GetAllStudent = _entities.FileDBCs.ToList();

            if (GetAllStudent.Count != 0)
            {
                foreach (var item in GetAllStudent)
                {
                    userData user = new userData
                    {

                        id = item.id,
                        Uid = item.Uid,
                        Email = item.Email,
                        ProfilePic = item.ProfilePic,
                        

                    };

                    if (item.Qualification.Equals(null))
                    {
                        user.Qualification = null;
                    }
                    else if (item.Qualification.Contains(','))
                    {
                        user.Qualification = item.Qualification.Split(',').ToList();
                    }
                    else
                    {
                        user.Qualification = new List<string>();
                        user.Qualification.Add(item.Qualification);
                    }
                    list.Add(user);
                }
                model.userList = list;
            }

            return View(model);
        }



        [HttpGet]
        public ActionResult AddItem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddItem(AddUserViewModel user)
        {
            if (ModelState.IsValid)
            {
                //Do SomeThing...

                FileDBC CheckStudent = _entities.FileDBCs.Where(x => x.Uid == user.Uid && x.Email == user.Email).FirstOrDefault();

                if (CheckStudent == null)
                {
                    var AddStudent = new FileDBC();
                    string ImageName = DateTime.Now.Ticks.ToString() + Path.GetFileName(user.ProfilePic.FileName.ToString());

                    string fileType = Path.GetFileName(user.ProfilePic.ContentType);
                    if (fileType == "jpg" || fileType == "jpeg" || fileType == "png")
                    {

                        string img = Server.MapPath("~/ProfileImage/" + ImageName);
                        user.ProfilePic.SaveAs(img);
                        AddStudent.Uid = user.Uid;
                        AddStudent.Email = user.Email;
                        AddStudent.ProfilePic = "/ProfileImage/" + ImageName;

                        var AllQualification = "";
                        foreach (var i in user.Qualification)
                        {
                            //if(i == "-1")
                            //{
                            //    continue;
                            //}
                            //else
                            //{
                            //    AllQualification += "," + i;
                            //}

                            AllQualification += "," + i;
                        }
                        AddStudent.Qualification = AllQualification.TrimStart(',');
                    }

                    //string img = Server.MapPath("~/ProfileImage/" + ImageName);
                    //user.ProfilePic.SaveAs(img);


                    //var AddStudent = new FileDBC();
                    //AddStudent.Uid = user.Uid;
                    //AddStudent.Email = user.Email;
                    //AddStudent.ProfilePic = "/ProfileImage/" + ImageName;

                    //var AllQualification = "";
                    //foreach(var i in user.Qualification)
                    //{
                    //    //if(i == "-1")
                    //    //{
                    //    //    continue;
                    //    //}
                    //    //else
                    //    //{
                    //    //    AllQualification += "," + i;
                    //    //}

                    //    AllQualification += "," + i;
                    //}
                    //AddStudent.Qualification = AllQualification.TrimStart(',');




                    _entities.FileDBCs.Add(AddStudent);
                    _entities.SaveChanges();

                    return RedirectToAction("DashBoard", "Work");
                }
                else
                {
                    return View();
                }


            }
            return View();
        }


        public ActionResult DashBoard()
        {
            return View();
        }




        //Edit: UserDetail
        [HttpGet]
        public ActionResult EditDetail(int id)
        {
            EditViewModel getDetial = new EditViewModel();
            if (ModelState.IsValid)
            {
                var get = _entities.FileDBCs.Where(x => x.id == id).FirstOrDefault();
                if (get != null)
                {
                    getDetial.id = get.id;
                    getDetial.Uid = get.Uid;
                    getDetial.Email = get.Email;
                    getDetial.img = get.ProfilePic;
                    if (get.Qualification.Equals(null))
                    {
                        getDetial.Qualification = null;
                    }
                    else if (get.Qualification.Contains(','))
                    {
                        getDetial.Qualification = get.Qualification.Split(',').ToList();
                    }
                    else
                    {
                        getDetial.Qualification = new List<string>();
                        getDetial.Qualification.Add(get.Qualification);
                    }

                }
                else
                {
                    return View();
                }
            }
            return View(getDetial);
        }

        [HttpPost]
        public ActionResult EditDetail(EditViewModel getDetail)
        {

            var isEmployeeExist = _entities.FileDBCs.Where(x => x.id == getDetail.id).FirstOrDefault();
            if (isEmployeeExist != null)
            {
                isEmployeeExist.Uid = getDetail.Uid;
                isEmployeeExist.Email = getDetail.Email;
                //isEmployeeExist.ProfilePic = getDetail.ProfilePic;
                var AllQualification = "";
                foreach (var i in getDetail.Qualification)
                {
                    //if(i == "-1")
                    //{
                    //    continue;
                    //}
                    //else
                    //{
                    //    AllQualification += "," + i;
                    //}

                    AllQualification += "," + i;
                }
                isEmployeeExist.Qualification = AllQualification.TrimStart(',');


                _entities.SaveChanges();
                return RedirectToAction("DashBoard", "Work");
            }

                //    string ImageName = DateTime.Now.Ticks.ToString() + Path.GetFileName(getDetail.ProfilePic.FileName.ToString());

                //    string fileType = Path.GetFileName(getDetail.ProfilePic.ContentType);
                //    if (fileType == "jpg" || fileType == "jpeg" || fileType == "png")
                //    {

                //        string img = Server.MapPath("~/ProfileImage/" + ImageName);
                //        getDetail.ProfilePic.SaveAs(img);
                //        isEmployeeExist.Uid = getDetail.Uid;
                //        isEmployeeExist.Email = getDetail.Email;
                //        isEmployeeExist.ProfilePic = "/ProfileImage/" + ImageName;

                //        var AllQualification = "";
                //        foreach (var i in getDetail.Qualification)
                //        {
                //            //if(i == "-1")
                //            //{
                //            //    continue;
                //            //}
                //            //else
                //            //{
                //            //    AllQualification += "," + i;
                //            //}

                //            AllQualification += "," + i;
                //        }
                //        isEmployeeExist.Qualification = AllQualification.TrimStart(',');
                //    }
                //    _entities.SaveChanges();
                //    return RedirectToAction("DashBoard", "Work");
                //}
                return View();
        }

    }
}