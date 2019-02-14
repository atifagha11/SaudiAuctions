﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Saudi_Auctions.BL;
using Saudi_Auctions.Wrappers;
using Saudi_Auctions.Models;
using Saudi_Auctions.Helpers;
using System.IO;
using System.Drawing;

namespace Saudi_Auctions.Controllers
{
    public class UploaderController : Controller
    {
        private AttachmentManager attachmentManager = new AttachmentManager();
        //
        // GET: /Uploader/
        public ActionResult Index()
        {
            var Files = attachmentManager.GetAllAttachments();
            return View(Files);
        }
        //
        // GET: /Uploader/Upload
        public ActionResult Upload()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("UploadSingle");
            }
            else
            {
                return new HttpNotFoundResult();
            }

        }
        //
        // POST: /Uploader/Upload
        [HttpPost]
        public JsonResult Upload(HttpPostedFileBase uploadedFile)
        {
            if (uploadedFile != null && uploadedFile.ContentLength > 0)
            {
                byte[] FileByteArray = new byte[uploadedFile.ContentLength];
                uploadedFile.InputStream.Read(FileByteArray, 0, uploadedFile.ContentLength);
                Attachment newAttchment = new Attachment();
                newAttchment.FileName = uploadedFile.FileName;
                newAttchment.FileType = uploadedFile.ContentType;
                newAttchment.FileContent = FileByteArray;
                OperationResult operationResult = attachmentManager.SaveAttachment(newAttchment);
                if (operationResult.Success)
                {
                    string HTMLString = CaptureHelper.RenderViewToString("_AttachmentItem", newAttchment, this.ControllerContext);
                    return Json(new
                    {
                        statusCode = 200,
                        status = operationResult.Message,
                        NewRow = HTMLString
                    }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new
                    {
                        statusCode = 400,
                        status = operationResult.Message,
                        file = uploadedFile.FileName
                    }, JsonRequestBehavior.AllowGet);

                }
            }
            return Json(new
            {
                statusCode = 400,
                status = "Bad Request! Upload Failed",
                file = string.Empty
            }, JsonRequestBehavior.AllowGet);
        }

        public FileContentResult DownloadAttachment(int id)
        {
            Attachment attachment = attachmentManager.GetAttachment(id);
            return File(attachment.FileContent, attachment.FileType, attachment.FileName);
        }
        [HttpPost]
        public ActionResult DeleteAttachment(int id)
        {
            OperationResult OperationResult = new OperationResult();
            OperationResult = attachmentManager.Delete(id);
            if (OperationResult.Success)
            {
                return Json(new { ID = id });
            }
            else
            {
                return Json(new { ID = "", message = OperationResult.Message });
            }
        }

        //
        // GET: /Uploader/Upload
        public ActionResult UplodMultiple()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("_UplodMultiple");
            }
            else
            {
                return new HttpNotFoundResult();
            }
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }


        [HttpPost]
        public JsonResult UplodMultiple(HttpPostedFileBase[] uploadedFiles)
        {
            List<Attachment> newAttachmentList = new List<Attachment>();
            foreach (var File in uploadedFiles)
            {
                if (File != null && File.ContentLength > 0)
                {
                    byte[] FileByteArray = new byte[File.ContentLength];
                    File.InputStream.Read(FileByteArray, 0, File.ContentLength);
                    Attachment newAttchment = new Attachment();
                    newAttchment.FileName = File.FileName;
                    newAttchment.FileType = File.ContentType;
                    newAttchment.FileContent = FileByteArray;

                    using (Image x = byteArrayToImage(FileByteArray))
                    {
                        x.Save(Server.MapPath("~/Uploads/" + newAttchment.FileName));
                    }
                    newAttachmentList.Add(newAttchment);
                }
            }
            OperationResult operationResult = attachmentManager.SaveAttachments(newAttachmentList);
            if (operationResult.Success)
            {
                string HTMLString = CaptureHelper.RenderViewToString("_AttachmentBulk", newAttachmentList, this.ControllerContext);
                return Json(new
                {
                    statusCode = 200,
                    status = operationResult.Message,
                    NewRow = HTMLString
                }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new
                {
                    statusCode = 400,
                    status = operationResult.Message
                }, JsonRequestBehavior.AllowGet);

            }
        }
    }
}
