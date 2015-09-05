using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConferenceManagementSystem.Models;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;
using DHTMLX.Common;
using ConferenceManagementSystem.DataAccessLayer;

namespace ConferenceManagementSystem.Controllers
{
    public class BasicSchedulerController : Controller
    {
        public ActionResult Index()
        {
            var scheduler = new DHXScheduler(this); //initializes dhtmlxScheduler
            var context = new ConferenceManagementContext();
            scheduler.Skin = DHXScheduler.Skins.Flat;
            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;
            //scheduler.Data.Parse(context.BreakTimes);
            //var box = scheduler.Lightbox.SetExternalLightbox("BasicScheduler/LightboxControl", 420, 195);
            //box.ClassName = "custom_lightbox";// if you want to apply your custom style
            return View(scheduler);
        }

        public ActionResult LightboxControl(int? id)
        {
            var context = new ConferenceManagementContext();
            var current = context.Schedules.SingleOrDefault(e => e.ScheduleId == id);
            ViewBag.TopicId = new SelectList(context.Topics, "TopicId", "Name");
            return View(current);
        }

        public ActionResult Data()
        {//events for loading to scheduler
            return new SchedulerAjaxData(new ConferenceManagementContext().Schedules);
        }

        public ActionResult Save(Schedule updatedEvent, FormCollection formData)
        {
            var action = new DataAction(formData);

            var context = new ConferenceManagementContext();

            if (formData["actionButton"] != null)
            {
                try
                {
                    if (formData["actionButton"] == "Save")
                    {
                        if (context.Schedules.SingleOrDefault(ev => ev.ScheduleId == action.SourceId) != null)
                        {
                            var breaktime = context.Schedules.SingleOrDefault(ev => ev.ScheduleId == action.SourceId);
                            TryUpdateModel(updatedEvent);
                        }
                        else
                        {
                            action.Type = DataActionTypes.Insert;
                            var breaktime = new Schedule();
                            breaktime.TopicId = 1;
                            breaktime.StartTime = updatedEvent.StartTime;
                            breaktime.EndDate = updatedEvent.EndDate;
                            breaktime.TopicId = updatedEvent.TopicId;
                            context.Schedules.Add(breaktime);
                        }
                    }
                    else if (formData["actionButton"] == "Delete")
                    {
                        action.Type = DataActionTypes.Delete;
                        updatedEvent = context.Schedules.SingleOrDefault(ev => ev.ScheduleId == updatedEvent.ScheduleId);
                        context.Schedules.Remove(updatedEvent);
                    }
                    context.SaveChanges();
                }
                catch (Exception a)
                {
                    action.Type = DataActionTypes.Error;
                }
            }
            else
            {
                action.Type = DataActionTypes.Error;
            }
            return (new SchedulerFormResponseScript(action, updatedEvent));
        }
    }
}