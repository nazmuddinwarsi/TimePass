using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using TimePass.DBData;
using TimePass.Models;

namespace TimePass.Controllers
{
	public class OperatorController : Controller
	{
		TimePassContext context = new TimePassContext();
		public IActionResult Index()
		{
            
            return View();
        }
		[HttpPost]
		public IActionResult Index(UserLogin Lg)
		{



			{
				var status = context.Oprators.Where(m => m.UserName == Lg.Username && m.Password == Lg.Password).FirstOrDefault();
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
					return RedirectToAction("Dashboard", "Operator");
				}

			}

			return View();
		}
		public IActionResult Dashboard()
		{
            string key = "UserName";
            var cookieValue = Request.Cookies[key];
			if (cookieValue == null)
			{
				return RedirectToAction("Index", "Operator");
			}
			else
			{

				OperatorDashboard operatorDashboard = new OperatorDashboard();
				var today2 = System.DateTime.Now.ToString("ddd, dd MMM yyyy ");
				TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
				DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
				//today2 = indianTime.ToString("ddd, dd MMM yyyy");
				today2 = indianTime.ToString("yyyy/MM/dd");
				operatorDashboard.ResultDate = Convert.ToDateTime(today2);
				return View(operatorDashboard);
			}

        }
		[HttpPost]
		public async Task<IActionResult> Dashboard(OperatorDashboard operatorDashboard)
		{
			var data = context.Results.FirstOrDefault(x => x.CreatedDate == operatorDashboard.ResultDate);
			if (data != null)
			{
				ViewBag.Status = 0;
			}
			else
			{
                await context.Results.ExecuteDeleteAsync();
				await context.Acitvities.ExecuteDeleteAsync();
				await context.Transactions.ExecuteDeleteAsync();
               
                TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
				//today2 = indianTime.ToString("ddd, dd MMM yyyy");
				DateTime s = operatorDashboard.ResultDate;
				TimeSpan ts = new TimeSpan(09, 00, 0);
				s = s.Date + ts;
				var today2 = s.ToString("yyyy/MM/dd hh:mm tt");
				string time = indianTime.ToString(today2);
               // today2 = indianTime.ToString("yyyy/MM/dd 09:00:00");
                DateTime dt = Convert.ToDateTime(today2);
				for (int i = 0; i <= 36; i++)
				{
					var add = new Result()
					{
						PlayTime = dt,
						CreatedDate = operatorDashboard.ResultDate,
						Active="N"

					};
					await context.Results.AddAsync(add);
					await context.SaveChangesAsync();
					dt = dt.AddMinutes(20);
				}
			}
			return View(operatorDashboard);
		}

		public IActionResult Result()

		{
			//var data = context.Results.Where(x => x.CreatedDate == System.DateTime.Today).ToList();
			////RegistrationList Rlist = new RegistrationList();
			//List<ResultModel> result = new List<ResultModel>();
			//if (data.Count > 0)
			//{
			//	foreach (var item in data)
			//	{
			//		var Rlist = new ResultModel()
			//		{
			//			Id = item.Id,
			//			ResultTime = item.PlayTime,
			//			ResultNumber = item.PlayNumber,
			//			ResultWinner = item.TotalWinner,

			//		};
			//		result.Add(Rlist);

			//	}

			//}
			ResultModel result = new ResultModel();
			var PlayTime = context.Results.Where(x => x.Active == "N").FirstOrDefault();
			if (PlayTime != null)
			{
				result.NextPlayTime = PlayTime.PlayTime;

				result.Id = PlayTime.Id;
			}
			//	var today2 = System.DateTime.Now.ToString("yyyy/MM/dd");
			TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
			DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
			//today2 = indianTime.ToString("ddd, dd MMM yyyy");
			//var today2 = indianTime.ToString("yyyy/MM/dd");
			
			DateTime dt = Convert.ToDateTime(indianTime.ToString("yyyy/MM/dd"));
			var data = context.Results.Where(x => x.CreatedDate == dt).ToList();
			if (data != null)
			{
				result.ResultsList = data.Select(s => new Models.ResultModel { Id = s.Id, ResultTime = s.PlayTime, ResultNumber = s.PlayNumber, ResultWinner = s.TotalWinner }).ToList();

			}

			return View(result);
		}
		[HttpPost]
		public IActionResult Result(int id, int Rnum)
		{
			var data = context.Results.FirstOrDefault(x => x.Id == id);
			if (data != null)
			{
				//data.PlayNumber=resultModel.;
				//data.TotalWinner = resultModel.ResultWinner;
				context.SaveChanges();
				TempData["JavaScriptFunction"] = string.Format("Update();");
			}

			return RedirectToAction("Result");
		}
		public IActionResult ResultEdit(int id, ResultModel result)
		{
			var data = context.Results.FirstOrDefault(x => x.Id == id);
			if (data != null)
			{
				//data.PlayNumber=resultModel.;
				//data.TotalWinner = resultModel.ResultWinner;
				context.SaveChanges();
				TempData["JavaScriptFunction"] = string.Format("Update();");
			}

			return RedirectToAction("Result");
		}
		public IActionResult test()
		{
			return View();
		}
		public IActionResult UpdateResult(int id)
		{
			var playData = context.Results.Where(x => x.Id == id).FirstOrDefault();
			ResultModel resultModel = new ResultModel();
			resultModel.Id = id;
			resultModel.ResultTime = playData.PlayTime;
			resultModel.zero = 0;
			var zero = context.Acitvities.Where(m => m.PlayTime == playData.PlayTime && m.PlayNumber == "0").ToList();
			if (zero != null)
			{
				foreach (var item in zero)
				{
					resultModel.zero = resultModel.zero + item.Coins;
				}


			}
			else
			{
				resultModel.zero = 0;
			}
			resultModel.one = 0;
			var one = context.Acitvities.Where(m => m.PlayTime == playData.PlayTime && m.PlayNumber == "1").ToList();
			if (one != null)
			{
				foreach (var item in one)
				{
					resultModel.one = resultModel.one + item.Coins;
				}


			}
			else
			{
				resultModel.one = 0;
			}
			resultModel.two = 0;
			var two = context.Acitvities.Where(m => m.PlayTime == playData.PlayTime && m.PlayNumber == "2").ToList();
			if (two != null)
			{
				foreach (var item in two)
				{
					resultModel.two = resultModel.two + item.Coins;
				}


			}
			else
			{
				resultModel.two = 0;
			}
			resultModel.three = 0;
			var three = context.Acitvities.Where(m => m.PlayTime == playData.PlayTime && m.PlayNumber == "3").ToList();
			if (three != null)
			{
				foreach (var item in three)
				{
					resultModel.three = resultModel.three + item.Coins;
				}


			}
			else
			{
				resultModel.three = 0;
			}
			resultModel.four = 0;
			var four = context.Acitvities.Where(m => m.PlayTime == playData.PlayTime && m.PlayNumber == "4").ToList();
			if (four != null)
			{
				foreach (var item in four)
				{
					resultModel.four = resultModel.four + item.Coins;
				}


			}
			else
			{
				resultModel.four = 0;
			}
			resultModel.five = 0;
			var five = context.Acitvities.Where(m => m.PlayTime == playData.PlayTime && m.PlayNumber == "5").ToList();
			if (five != null)
			{
				foreach (var item in five)
				{
					resultModel.five = resultModel.five + item.Coins;
				}


			}
			else
			{
				resultModel.five = 0;
			}

			resultModel.six = 0;
			var six = context.Acitvities.Where(m => m.PlayTime == playData.PlayTime && m.PlayNumber == "6").ToList();
			if (six != null)
			{
				foreach (var item in six)
				{
					resultModel.six = resultModel.six + item.Coins;
				}


			}
			else
			{
				resultModel.six = 0;
			}

			resultModel.seven = 0;
			var seven = context.Acitvities.Where(m => m.PlayTime == playData.PlayTime && m.PlayNumber == "7").ToList();
			if (seven != null)
			{
				foreach (var item in seven)
				{
					resultModel.seven = resultModel.seven + item.Coins;
				}


			}
			else
			{
				resultModel.seven = 0;
			}

			resultModel.eight = 0;
			var eight = context.Acitvities.Where(m => m.PlayTime == playData.PlayTime && m.PlayNumber == "8").ToList();
			if (eight != null)
			{
				foreach (var item in eight)
				{
					resultModel.eight = resultModel.eight + item.Coins;
				}


			}
			else
			{
				resultModel.eight = 0;
			}

			resultModel.nine = 0;
			var nine = context.Acitvities.Where(m => m.PlayTime == playData.PlayTime && m.PlayNumber == "9").ToList();
			if (nine != null)
			{
				foreach (var item in nine)
				{
					resultModel.nine = resultModel.nine + item.Coins;
				}


			}
			else
			{
				resultModel.nine = 0;
			}

			return View(resultModel);

			 


		}

		[HttpPost]
		public IActionResult UpdateResult(ResultModel resultModel)
		{
			var data = context.Results.Where(m => m.Id == resultModel.Id).FirstOrDefault();
			if (data != null)
			{
				data.PlayNumber=resultModel.ResultNumber;
				data.TotalWinner = resultModel.ResultWinner;
				context.SaveChanges();
			}
			TempData["JavaScriptFunction"] = string.Format("Update();");



			return RedirectToAction("Result");
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
            return RedirectToAction("Index", "Operator");
        }
        public IActionResult ServerDown()
        {
            var sActive = context.Paras.FirstOrDefault();
            if (sActive != null)
            {
                string key = "UserID";
                string value = string.Empty;
                CookieOptions options = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                Response.Cookies.Append(key, value, options);
                if (sActive.Serveractive == 0)
                {
                    sActive.Serveractive = 1;
                }
                else
                {
                    sActive.Serveractive = 0;
                }
                context.SaveChanges();


            }
            return RedirectToAction("Index", "Home");


        }
		public IActionResult updateResultUser(int id)
		{
			var data = context.Results.Where(x => x.Id == id).FirstOrDefault();
			if (data != null)
			{
				data.Active = "Y";
				context.SaveChanges();
				var playtime = data.PlayTime;
				var playNumber = data.PlayNumber;
				var activity = context.Acitvities.Where(m => m.PlayTime == playtime).ToList();
				if (activity != null)
				{

					foreach (var item in activity)
					{
						if (item.PlayNumber == playNumber)
						{
							var reg = context.Registrations.Where(x => x.UserId == item.UserId).FirstOrDefault();
							if (reg != null)
							{
								reg.Coins = reg.Coins + (item.Coins * 10);
								context.SaveChanges();
							}
						}
					}
				}
			}


			return RedirectToAction("Result");
		}
	}
}
