using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Mono.TextTemplating;
using System.Diagnostics.Metrics;
using TimePass.DBData;
using TimePass.Models;

namespace TimePass.Controllers
{
    public class UserController : Controller
    {
        TimePassContext context = new TimePassContext();
        public IActionResult Index()
        {
            UserLogin userLogin = new UserLogin();
            var sActive = context.Paras.FirstOrDefault();
            if (sActive.Serveractive == 0)
            {


               
                return RedirectToAction("Index", "Home");
            }
            string key = "UserID";
            var cookieValue = Request.Cookies[key];
            if (cookieValue != null)
            {
                UserDashboard userDashboard = new UserDashboard();
                var PlayTime = context.Results.Where(x => x.Active == "N" ).FirstOrDefault();
                if (PlayTime != null)
                {
                    userDashboard.NextPlayTime = PlayTime.PlayTime;
					userDashboard.PlayTime = PlayTime.PlayTime.ToString("yyyy-MM-dd hh:mm tt");
                    userDashboard.Id= PlayTime.Id;
                }
                var d = System.DateTime.Today.ToString("yyyy/MM/dd");
                var TodayResult = context.Results.Where(x => x.Active == "Y").ToList();
                //  stateViewModel.StateLists = states1.Select(s => new Models.StateList { Id = s.Id, State1 = s.State1, StateCode = s.StateCode }).ToList();

                if (TodayResult != null)
                {
                    userDashboard.PlayResult = TodayResult.Select(s => new Models.ResultModel { ResultTime = s.PlayTime, ResultNumber = s.PlayNumber, ResultWinner = s.TotalWinner }).ToList();
                }
                var data = context.Registrations.FirstOrDefault(m => m.UserId == Convert.ToInt32(cookieValue));
                if (data != null)
                {
                    userDashboard.Id = data.UserId;
                    userDashboard.photo = data.Photo;
                    userDashboard.Coins = data.Coins;
                }

                return View(userDashboard);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserDashboard user)
        {
            string key = "UserID";
			TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
			DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

			var cookieValue = Request.Cookies[key];
            {
                //if (ModelState.IsValid)
                {
                    var Add = new Acitvity()
                    {
                        UserId = Convert.ToInt32(cookieValue),
                        PlayNumber = user.PlayNumber,
                        PlayTime = Convert.ToDateTime(user.PlayTime),
                        Coins = user.PlayCoins,
                        CreatedDate = Convert.ToDateTime(indianTime.ToString("yyyy/MM/dd")),
                    };
                    await context.Acitvities.AddAsync(Add);
                    await context.SaveChangesAsync();
                    var CoinUpdate = context.Registrations.FirstOrDefault(x => x.UserId == Convert.ToInt32(cookieValue));
                    if (CoinUpdate != null)
                    {

                        CoinUpdate.Coins = CoinUpdate.Coins - user.PlayCoins;

                        await context.SaveChangesAsync();





                    }

                    // TempData["JavaScriptFunction"] = string.Format("Success();");
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult GameActivity()

        {
            string key = "UserID";
            var cookieValue = Request.Cookies[key];
            if (cookieValue != null)
            {
				TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
				DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
				var d = indianTime.ToString("yyyy/MM/dd");
                var TodayActivity = context.Acitvities.Where(x => x.CreatedDate == Convert.ToDateTime(d) && x.UserId == Convert.ToInt32(cookieValue)).ToList();
                //  stateViewModel.StateLists = states1.Select(s => new Models.StateList { Id = s.Id, State1 = s.State1, StateCode = s.StateCode }).ToList();

                if (TodayActivity != null)
                {
                    List<UserActivity> userActivities = new List<UserActivity>();
                    if (TodayActivity.Count > 0)
                    {
                        foreach (var item in TodayActivity)
                        {
                            var Rlist = new UserActivity()
                            {
                                UserId = item.UserId,
                                PlayNumber = item.PlayNumber,
                                PlayTime = item.PlayTime,

                                coins = item.Coins

                            };
                            userActivities.Add(Rlist);

                        }
                    }

                    return View(userActivities);
                }

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public IActionResult Reward()

        {
            string key = "UserID";
            var cookieValue = Request.Cookies[key];
            if (cookieValue != null)
            {
				TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
				DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
				var d = indianTime.ToString("yyyy/MM/dd");
                var Reward = context.RptRewards.Where(x=> x.Userid == Convert.ToInt32(cookieValue)).ToList();
                //  stateViewModel.StateLists = states1.Select(s => new Models.StateList { Id = s.Id, State1 = s.State1, StateCode = s.StateCode }).ToList();

                if (Reward != null)
                {
                    List<RewardModel> userActivities = new List<RewardModel>();
                    if (Reward.Count > 0)
                    {
                        foreach (var item in Reward)
                        {
                            var Rlist = new RewardModel()
                            {
                                UserId = item.Userid,
                                PlayNumber = item.Playnumber,
                                PlayTime = item.Playtime,

                                coins = item.Coin

                            };
                            userActivities.Add(Rlist);

                        }
                    }

                    return View(userActivities);
                }

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

     

        public IActionResult logout()
        {
            string key = "UserID";
            string value = string.Empty;
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1)
            };
            Response.Cookies.Append(key, value, options);
            return RedirectToAction("Index","Home");
        }
        public IActionResult UserTransaction()
        {
            var sActive = context.Paras.FirstOrDefault();
            if (sActive.Serveractive == 0)
            {



                return RedirectToAction("Index", "Home");
            }
            string key = "UserID";
            var cookieValue = Request.Cookies[key];
            if (cookieValue != null)
            {
                var data = context.Transactions.Where(x => x.Userid == Convert.ToInt32(cookieValue)).OrderByDescending(x => x.TTime).ToList();
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
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult test()
        {
            return View();
        }
    }
}
