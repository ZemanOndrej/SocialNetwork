using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PL.Models;

namespace PL.Controllers
{
	[Authorize]
	public class ManageController : Controller
	{
	   


		public ActionResult Index(ManageMessageId? message)
		{
			ViewBag.StatusMessage =
				message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
				: message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
				: message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
				: message == ManageMessageId.Error ? "An error has occurred."
				: message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
				: message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
				: "";

			var userId = User.Identity.GetUserId();
//			var model = new IndexViewModel
//			{
//				HasPassword = HasPassword(),
//				PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
//				TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
//				Logins = await UserManager.GetLoginsAsync(userId),
//				BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
//			};
			return View();
		}

		public ActionResult ChangePassword()
		{
			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public  ActionResult ChangePassword(ChangePasswordViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			
			return View(model);
		}


		public ActionResult SetPassword()
		{
			return View();
		}

		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult SetPassword(SetPasswordViewModel model)
		{
			
			return View(model);
		}


		
#region Helpers

		public enum ManageMessageId
		{
			AddPhoneSuccess,
			ChangePasswordSuccess,
			SetTwoFactorSuccess,
			SetPasswordSuccess,
			RemoveLoginSuccess,
			RemovePhoneSuccess,
			Error
		}

#endregion
	}
}