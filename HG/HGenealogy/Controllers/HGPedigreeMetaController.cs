using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HGenealogy.Services;
using HGenealogy.Models.HGPedigree;
using HGenealogy.Domain;
using HGenealogy.Extensions;
using AutoMapper;

namespace HGenealogy.Controllers
{
    public class HGPedigreeMetaController : BasePublicController
    {
        private readonly IHGPedigreeMetaService _hGPedigreeMetaService;

        public HGPedigreeMetaController(
            IHGPedigreeMetaService hGPedigreeMetaService
            )
        {
            _hGPedigreeMetaService = hGPedigreeMetaService;
            mappingInit();//暫時先這樣處理
        }

        #region mappingInit
        /// <summary>
        /// 建立這個controller中所有的entity和VM的映射關係
        /// </summary>
        private void mappingInit()
        {
            Mapper.CreateMap<HGPedigreeMeta, HGPedigreeMetaModel>();
            Mapper.CreateMap<HGPedigreeMetaModel, HGPedigreeMeta>();
                    
        }
        #endregion


        // GET: HGPedigreeMeta
        public ActionResult Index()
        {
            ViewBag.Title = "族譜瀏覽";
            List<HGPedigreeMetaModel> modelList = new List<HGPedigreeMetaModel>();
            var result = _hGPedigreeMetaService.GetAllHGPedigreeMeta();
            if (result == null)
                return View(modelList);
            foreach (HGPedigreeMeta model in result)
            {
                modelList.Add(model.ToModel());
            }

            return View(modelList);
        }

        //只取前十筆
        public ActionResult HGPedigreeMetaNavigation(int currentHGPedigreeId)
        {

            //var model = new CategoryNavigationModel
            //{
            //    CurrentCategoryId = activeCategoryId,
            //    Categories = cachedModel
            //};

            //return PartialView(model);

            return null;
        }

        //新增族譜基本資料
        public ActionResult DoCreate()
        {
            HGPedigreeMetaModel model = new HGPedigreeMetaModel();
            model.PublishDate = System.DateTime.Now;
            ViewBag.Title = "建立您的族譜";
            return PartialView("CreateOrUpdate", model);
        }

        //修改族譜基本資料
        public ActionResult DoUpdate(int id)
        {
            var hGPedigreeMeta = _hGPedigreeMetaService.GetHGPedigreeMetaById(id);
            var model = hGPedigreeMeta.ToModel();
            ViewBag.Title = "修改您的族譜";

            return PartialView("CreateOrUpdate", model);
        }

        [HttpPost]
        public ActionResult SaveHGPedigreeMeta(HGPedigreeMetaModel model)
        {
            HGPedigreeMeta hGPedigreeMeta = new HGPedigreeMeta();
            hGPedigreeMeta = model.ToEntity();
            if (hGPedigreeMeta.Id == 0)//新增
            {
                hGPedigreeMeta.Editor = "???";
                hGPedigreeMeta.Volumes = 0;
                hGPedigreeMeta.Pages = 0;
                hGPedigreeMeta.FamilyName = "abc";
                hGPedigreeMeta.OriginalAncestor = "";
                hGPedigreeMeta.DateMoveToTaiwan = "";
                hGPedigreeMeta.TotalGenerations = 0;
                hGPedigreeMeta.GenerationToTaiwan = 0;
                hGPedigreeMeta.IsPublic = false;
                hGPedigreeMeta.CreatedOnUtc = System.DateTime.Now;
                hGPedigreeMeta.CreatedWho = "???";
            }
            else
            {
                hGPedigreeMeta = _hGPedigreeMetaService.GetHGPedigreeMetaById(hGPedigreeMeta.Id);
                hGPedigreeMeta = model.ToEntity(hGPedigreeMeta);
            }
            hGPedigreeMeta.UpdatedOnUtc = System.DateTime.Now;
            hGPedigreeMeta.UpdatedWho = "???";

            if (hGPedigreeMeta.Id == 0)//新增
                _hGPedigreeMetaService.InsertHGPedigreeMeta(hGPedigreeMeta);
            else
                _hGPedigreeMetaService.UpdateHGPedigreeMeta(hGPedigreeMeta);

            return RedirectToAction("Index");
            //return Json(new { success = true, message = "" });
        }

        //[HttpPost]
        //public ActionResult DeleteHGPedigreeMeta(int id)
        //{
        //    try
        //    {

        //        var hGPedigreeMeta = _hGPedigreeMetaService.GetHGPedigreeMetaById(id);
        //        var model = hGPedigreeMeta.ToModel();
        //        _hGPedigreeMetaService.DeleteHGPedigreeMeta(hGPedigreeMeta);
        //        return Json(new { success = true, message = "" });
        //    }
        //    catch (Exception ex )
        //    {
        //        return Json(new { success = false, message = ex.Message });
        //    }
        //}

        //[HttpPost]
        //public ActionResult DoHGPedigreeInfo(int id)
        //{
        //    try
        //    {
        //        //var url = Url.Action("Index", "HGPedigreeInfo");
        //        var url = @"/HGPedigreeInfo/Index";
        //        System.Web.HttpContext.Current.Session["HGPedigreeId"] = id; 

        //        return Json(new { success = true, message = "",url=url });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = ex.Message });
        //    }
        //}
    }
}