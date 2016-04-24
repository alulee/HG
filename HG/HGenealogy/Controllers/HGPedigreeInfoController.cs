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
    public class HGPedigreeInfoController : Controller
    {
        private readonly IHGPedigreeMetaService _hGPedigreeMetaService;
        private readonly IHGPedigreeInfoService _hGPedigreeInfoService;
        private readonly int? _hGPedigreeId;

        public HGPedigreeInfoController(
            IHGPedigreeMetaService hGPedigreeMetaService,
            IHGPedigreeInfoService hGPedigreeInfoService
            )
        {
            _hGPedigreeMetaService = hGPedigreeMetaService;
            _hGPedigreeInfoService = hGPedigreeInfoService;

            mappingInit();//暫時先這樣處理
        }

        #region mappingInit
        /// <summary>
        /// 建立這個controller中所有的entity和VM的映射關係
        /// </summary>
        private void mappingInit()
        {
            Mapper.CreateMap<HGPedigreeInfo, HGPedigreeInfoModel>();
            Mapper.CreateMap<HGPedigreeInfoModel, HGPedigreeInfo>();

        }
        #endregion

        // GET: HGPedigreeInfo
        public ActionResult Index(int hGPedigreeID, string infoType)
        {
            if (string.IsNullOrWhiteSpace(infoType))
                infoType = "Preface";

            if (hGPedigreeID == 0)
                Redirect(Url.Action("HGPedigreeMeta", "Index"));

            var result = GetHGPedigreeInfoList(hGPedigreeID, infoType);
            if (result == null)
                result = new List<HGPedigreeInfoModel>();

            var hGPedigreeMeta = _hGPedigreeMetaService.GetHGPedigreeMetaById(hGPedigreeID);
            ViewBag.Title = string.Concat(hGPedigreeMeta.Title,"族譜資料");
            ViewBag.currentHGPedigreeID = hGPedigreeID;
            ViewBag.currentInfoType = infoType;
            ViewBag.infoTypeDic = getInfoTypeDic();
            return View(result);
        }

        //public ActionResult List(string infoType, int hGPedigreeID)
        //{

        //    ViewBag.HGPedigreeID = hGPedigreeID;
        //    ViewBag.infoType = infoType;
        //    return PartialView("_List", GetHGPedigreeInfoList(hGPedigreeID, infoType));
        //}

        private List<HGPedigreeInfoModel> GetHGPedigreeInfoList(int hGPedigreeID, string infoType)
        {
            var tempList = _hGPedigreeInfoService.GetAllHGPedigreeInfo()
                                .Where(x => x.HGPedigreeID == hGPedigreeID && x.InfoType == infoType)
                                .ToList()
                                ;
            List<HGPedigreeInfoModel> result = new List<HGPedigreeInfoModel>();
            var hGPedigreeMeta = _hGPedigreeMetaService.GetHGPedigreeMetaById(Convert.ToInt16(_hGPedigreeId));
            foreach (HGPedigreeInfo entity in tempList)
            {
                var model = entity.ToModel();
                model.hGPedigreeMeta = hGPedigreeMeta;
                //model.infoTypeDic = getInfoTypeDic();
                //model.currentHGPedigreeID = hGPedigreeID;
                //model.InfoType = infoType;
                result.Add(model);
            }


            return result;
        }

        public ActionResult DoCreate(int hGPedigreeID, string infoType)
        {
            HGPedigreeInfoModel model = new HGPedigreeInfoModel();
            model.HGPedigreeID = hGPedigreeID;
            model.InfoType = infoType;

            var infoTypeDic = getInfoTypeDic();
            var infoTypeC=infoTypeDic.Where(p=>p.Key==infoType).FirstOrDefault().Value;
            ViewBag.Title = string.Concat(infoTypeC, "-新增");
            ViewBag.currentHGPedigreeID = hGPedigreeID;
            ViewBag.currentInfoType = infoType;
            ViewBag.infoTypeDic = infoTypeDic;

            return View("CreateOrUpdate", model);
        }

        public ActionResult DoUpdate(int id)
        {
            var hGPedigreeInfo = _hGPedigreeInfoService.GetHGPedigreeInfoById(id);
            var model = hGPedigreeInfo.ToModel();
            var infoTypeDic = getInfoTypeDic();
            var infoTypeC = infoTypeDic.Where(p => p.Key ==model.InfoType).FirstOrDefault().Value;
            ViewBag.Title = string.Concat(infoTypeC, "-修改");
            ViewBag.currentHGPedigreeID = model.HGPedigreeID;
            ViewBag.currentInfoType = model.InfoType;
            ViewBag.infoTypeDic = infoTypeDic;

            ViewBag.infoTypeSelectList = getInfoTypeSelectList(model.InfoType);
            return View("CreateOrUpdate", model);
        }

        public JsonResult DoDelete(int id)
        {
            try
            {
                var hGPedigreeInfo = _hGPedigreeInfoService.GetHGPedigreeInfoById(id);
                _hGPedigreeInfoService.DeleteHGPedigreeInfo(hGPedigreeInfo);
                return Json(new { success = true});
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult SaveHGPedigreeInfo(HGPedigreeInfoModel model)
        {
            HGPedigreeInfo hGPedigreeInfo = new HGPedigreeInfo();
            hGPedigreeInfo = model.ToEntity();
            if (hGPedigreeInfo.Id == 0)//新增
            {
                hGPedigreeInfo.CreatedOnUtc = System.DateTime.Now;
                hGPedigreeInfo.CreatedWho = "???";
            }
            else
            {
                hGPedigreeInfo = _hGPedigreeInfoService.GetHGPedigreeInfoById(hGPedigreeInfo.Id);
                hGPedigreeInfo = model.ToEntity(hGPedigreeInfo);
            }
            hGPedigreeInfo.UpdatedOnUtc = System.DateTime.Now;
            hGPedigreeInfo.UpdatedWho = "???";

            if (hGPedigreeInfo.Id == 0)//新增
                _hGPedigreeInfoService.InsertHGPedigreeInfo(hGPedigreeInfo);
            else
                _hGPedigreeInfoService.UpdateHGPedigreeInfo(hGPedigreeInfo);

            return RedirectToAction("Index", "HGPedigreeInfo", new {  hGPedigreeID=model.HGPedigreeID, infoType=model.InfoType});
        }

        private SelectList getInfoTypeSelectList(string defalutValue)
        {
            SelectList returnSelectList = null;

            IDictionary<string, string> infoTypeDic = getInfoTypeDic();

            if (string.IsNullOrWhiteSpace(defalutValue))
                returnSelectList = new SelectList(infoTypeDic, "Key", "Value" );
            else
                returnSelectList = new SelectList(infoTypeDic, "Key", "Value", defalutValue);
            return returnSelectList;
        }

        private IDictionary<string,string> getInfoTypeDic()
        {
            IDictionary<string, string> infoTypeDic = new Dictionary<string, string>();
            infoTypeDic.Add("Preface", "序言凡例");
            infoTypeDic.Add("History", "家族歷史");
            infoTypeDic.Add("RootDescription", "姓氏源流");
            infoTypeDic.Add("Personage", "名人傳記");
            infoTypeDic.Add("NameRank", "字輩昭穆");
            infoTypeDic.Add("Precept", "族規家訓");
            infoTypeDic.Add("Tomb", "祠宇墳塋");
            return infoTypeDic;
        }
    }
       
}