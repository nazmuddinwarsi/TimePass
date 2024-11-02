using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimePass.DBData;
using TimePass.Models;


namespace TimePass.Controllers
{
	public class AdminController : Controller
	{
		IWebHostEnvironment webHostEnvironment;
		TimePassContext context = new TimePassContext();
		public AdminController(IWebHostEnvironment HC)
		{
			webHostEnvironment = HC;
		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Index(UserLogin Lg)
		{
			{
				var status = context.Adminlogins.Where(m => m.UserName == Lg.Username && m.Password == Lg.Password).FirstOrDefault();
				if (status == null)
				{
					ViewBag.Status = 0;
				}
				else
				{

					var LDetails = new UserLogin
					{

						Username = status.UserName,
						Password = status.Password
					};

					//HttpContext.Session.SetInt32("AccountID", LDetails.Id);
					//HttpContent.Session.SetString("Username",LDetails.Username);
					string key = "UserName";
					string value = LDetails.Username.ToString();
					CookieOptions options = new CookieOptions
					{
						Expires = DateTime.Now.AddDays(5)
					};
					Response.Cookies.Append(key, value, options);
					return RedirectToAction("DashboardAdmin", "Admin");
				}

			}

			return View();
		}
		public IActionResult DashboardAdmin()
		{
			//Dashboard
			string key = "UserName";
			var cookieValue = Request.Cookies[key];
			if (cookieValue == null)
			{
				return RedirectToAction("Index", "Admin");
			}
			return View();
		}
		[HttpGet]
		public async Task<IActionResult> RegistrationList(int id)
		{
			string key = "UserName";
			var cookieValue = Request.Cookies[key];
			if (cookieValue != null)
			{
				if (id != 0)
				{

					var RegData = await context.Registrations.FindAsync(id);

					//	RegistrationList Rlist = new RegistrationList();


					//List<RegistrationList> RegList = new List<RegistrationList>();
					if (RegData != null)
					{
						context.Registrations.Remove(RegData);
						await context.SaveChangesAsync();
						TempData["JavaScriptFunction"] = string.Format("Delete();");

					}
					//return RedirectToAction("RegistrationList/../", new { AType = tType });
					return RedirectToAction("RegistrationList", "Admin");
				}
				else
				{
					var RegData = context.Registrations.ToList();
					//RegistrationList Rlist = new RegistrationList();
					List<RegistrationList> RegList = new List<RegistrationList>();
					if (RegData.Count > 0)
					{
						foreach (var item in RegData)
						{
							var Rlist = new RegistrationList()
							{
								UserId = item.UserId,
								Name = item.Name,
								Mobile = item.Mobile,
								Address = item.Address,
								Photo = item.Photo,
								Username = item.Username,
								Password = item.Password,
								CreatedDate = item.CreatedDate,
								Coins = item.Coins

							};
							RegList.Add(Rlist);

						}

					}


					return View(RegList);
				}
			}
			else
			{ return RedirectToAction("Index", "Admin"); }

		}
		[HttpPost]
		public async Task<IActionResult> RegistrationList(RegistrationList registration)
		{

			var RegData = await context.Registrations.FindAsync(registration.UserId);
			//RegistrationList Rlist = new RegistrationList();
			//List<RegistrationList> RegList = new List<RegistrationList>();
			if (RegData != null)
			{
				context.Registrations.Remove(RegData);
				await context.SaveChangesAsync();
				TempData["JavaScriptFunction"] = string.Format("Delete();");

			}

			return RedirectToAction("RegistrationList");
		}
		[HttpGet]
		public async Task<IActionResult> Registation(int ID)
		{
			//ViewData["AType"] = TType;

			string key = "UserName";
			var cookieValue = Request.Cookies[key];
			if (cookieValue != null)
			{
				ViewData["ID"] = ID;
				if (ID == 0)
				{
					RegistrationList registrationModel = new RegistrationList();
					//registrationModel.Reg.TType= RouteData.Values["id"].ToString();
					registrationModel = new RegistrationList();
					return View(registrationModel);

				}
				else
				{
					var RegData = await context.Registrations.FirstOrDefaultAsync(m => m.UserId == ID);

					if (RegData != null)
					{
						//RegistrationList registration = new RegistrationList();
						RegistrationList registrationModel = new RegistrationList();

						registrationModel = new RegistrationList()

						{
							UserId = RegData.UserId,
							Name = RegData.Name,
							Mobile = RegData.Mobile,
							Address = RegData.Address,
							Username = RegData.Username,
							Password = RegData.Password,
							//Coins=RegData.Coins,
							Photo = RegData.Photo

						};

						//ViewData["AType"] = registrationModel.Reg.TType;


						return View(registrationModel);

					}




					else
					{
						return NotFound();
					}
				}
				return View();
			}
			else
			{ return RedirectToAction("Index", "Admin"); }
		}

		[HttpPost]
		public async Task<IActionResult> Registation(RegistrationList registration)
		{


			var RegUpdate = await context.Registrations.FindAsync(((int)registration.UserId));
			if (RegUpdate != null)
			{

				RegUpdate.Name = registration.Name;
				RegUpdate.Mobile = registration.Mobile;
				RegUpdate.Address = registration.Address;
				RegUpdate.Username = registration.Username;
				RegUpdate.Password = registration.Password;
				if (RegUpdate.Coins == null)
				{
					RegUpdate.Coins = 0;
				}
				if (registration.Coins == null)
				{
					registration.Coins = 0;
				}
				RegUpdate.Coins = RegUpdate.Coins + registration.Coins;
				TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
				DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
				var AddTrans = new Transaction()
				{
					Userid = registration.UserId,
					Coins = registration.Coins,
					TTime = indianTime
				};

				await context.Transactions.AddAsync(AddTrans);
				await context.SaveChangesAsync();


				//if (registration.imgPhoto != null)
				//{
				//	string folder = Path.Combine(webHostEnvironment.WebRootPath, "Upload");
				//	string filename = Guid.NewGuid() + "_" + registration.imgPhoto.FileName;
				//	string FilePath = Path.Combine(folder, filename);
				//	registration.imgPhoto.CopyTo(new FileStream(FilePath, FileMode.Create));
				//	RegUpdate.Photo = filename;

				//}



				context.SaveChanges();
				TempData["JavaScriptFunction"] = string.Format("Update();");



				return RedirectToAction("RegistrationList");
			}

			else
			{
				//var t = Context.Request.RouteValues["id"];


				{
					if (registration.imgPhoto != null)
					{
						string folder = Path.Combine(webHostEnvironment.WebRootPath, "Upload");
						string filename = Guid.NewGuid() + "_" + registration.imgPhoto.FileName;
						string FilePath = Path.Combine(folder, filename);
						registration.imgPhoto.CopyTo(new FileStream(FilePath, FileMode.Create));

						var AddData = new Registration()
						{

							Name = registration.Name,
							Mobile = registration.Mobile,

							Address = registration.Address,
							Username = registration.Username,
							Password = registration.Password,
							Coins = registration.Coins,
							Photo = filename,
							CreatedDate = DateTime.Now,
						};
						await context.Registrations.AddAsync(AddData);
						await context.SaveChangesAsync();
					}
					else
					{
						var AddData = new Registration()
						{

							Name = registration.Name,
							Mobile = registration.Mobile,

							Address = registration.Address,
							Username = registration.Username,
							Password = registration.Password,
							Coins = registration.Coins,

							CreatedDate = DateTime.Now,
						};
						await context.Registrations.AddAsync(AddData);
						await context.SaveChangesAsync();
					}

					TempData["JavaScriptFunction"] = string.Format("Success();");


				}
			}
			return RedirectToAction("RegistrationList");

		}
		public IActionResult logout()
		{
			string key = "UserName";
			string value = string.Empty;
			CookieOptions options = new CookieOptions
			{
				Expires = DateTime.Now.AddDays(-1)
			};
			Response.Cookies.Append(key, value, options);
			return RedirectToAction("Index", "Admin");
		}
		public IActionResult Transaction(int id)
		{

			var data = context.Transactions.Where(x => x.Userid == id).OrderByDescending(x => x.TTime).ToList();
			List<TransactionModel> transactionModels = new List<TransactionModel>();

			if (data.Count > 0)
			{
				foreach (var item in data)
				{
					var Rlist = new TransactionModel()
					{
						Coins = item.Coins,
						TTime = item.TTime,
						Userid = item.Userid,

					};
					transactionModels.Add(Rlist);

				}

			}

			return View(transactionModels);
		}
	}
}
