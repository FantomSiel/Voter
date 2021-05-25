using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voter.Dto.Dtos;
using Voter.Dto.Models;
using Voter.Dto.Requests.Poll;
using Voter.Dto.Requests.Question;
using Voter.Dto.Requests.User;
using Voter.Dto.Requests.Variant;
using Voter.Service.Attributes;

namespace Voter.Service.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;
        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserProfile data)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(new SignInRequest()
                {
                    Header = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(data.Login + ":" + data.Password))
                });

                HttpContext.Session.Set("UserId", Encoding.UTF8.GetBytes(response.UserId));
                HttpContext.Session.Set("UserName", Encoding.UTF8.GetBytes(response.Name));
                HttpContext.Session.Set("UserToken", Encoding.UTF8.GetBytes(response.Token));

                return RedirectToAction("Main");
            }
            return View(data);
        }

        [Authorize]
        [HttpGet("main")]
        public async Task<IActionResult> Main(int page = 1, bool isOwn = false)
        {
            var response = await _mediator.Send(new GetPollListRequest()
            {
                Limit = 20,
                Offset = (page - 1) * 20,
                UserId = isOwn ? ((UserDto)HttpContext.Items["User"]).Id.ToString() : null
            });

            return View(new MainPageModel()
            {
                Polls = response.Polls,
                Page = page,
                IsOwn = isOwn
            });
        }

        [Authorize]
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute] string id)
        {
            var response = await _mediator.Send(new GetPollRequest()
            {
                Id = id
            });

            return View(new EditModel()
            {
                PollId = id,
                Name = response.Name,
                Description = response.Description,
                Questions = response.Questions.ToList()
            });
        }

        [Authorize]
        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute] string id, EditModel model)
        {
            var request = new UpdatePollRequest()
            {
                Id = id,
                Name = model.Name,
                Description = model.Description
            };

            await _mediator.Send(request);

            return RedirectToAction("Edit", "Home", id);
        }

        [Authorize]
        [HttpGet("EditQuestion/{id}")]
        public async Task<IActionResult> EditQuestion([FromRoute] string id)
        {
            var response = await _mediator.Send(new GetQuestionRequest()
            {
                Id = id
            });

            return View(new EditQuestionModel()
            {
                QuestionId = id,
                Text = response.Text,
                Variants = response.Variants
            });
        }

        [Authorize]
        [HttpPost("EditQuestion/{id}")]
        public async Task<IActionResult> EditQuestion([FromRoute] string id, [FromQuery] string variantId, [FromQuery] int index, EditQuestionModel model)
        {
            if (!string.IsNullOrEmpty(variantId))
            {
                var request = new UpdateVariantRequest()
                {
                    Id = variantId,
                    Text = model.Variants[index].Text,
                };

                await _mediator.Send(request);
            }
            else
            {
                var request = new UpdateQuestionRequest()
                {
                    Id = id,
                    Text = model.Text,
                };

                await _mediator.Send(request);
            }

            return RedirectToAction("EditQuestion", "Home", id);
        }

        [Authorize]
        [HttpPost("DeleteQuestion/{id}")]
        public async Task<IActionResult> DeleteQuestion([FromRoute] string id, [FromQuery] string questionId)
        {
            var request = new DeleteQuestionRequest()
            {
                Id = questionId,
            };

            await _mediator.Send(request);
            return RedirectToAction("Edit", "Home", new { id = id });
        }

        [Authorize]
        [HttpPost("DeleteVariant/{id}")]
        public async Task<IActionResult> DeleteVariant([FromRoute] string id, [FromQuery] string variantId)
        {
            var request = new DeleteVariantRequest()
            {
                Id = variantId,
            };

            await _mediator.Send(request);
            return RedirectToAction("EditQuestion", "Home", new { id = id });
        }

        [Authorize]
        [HttpPost("AddVariant/{id}")]
        public async Task<IActionResult> AddVariant([FromRoute] string id)
        {
            var request = new AddVariantRequest()
            {
                QuestionId = id,
                Text = "Paste your text here ..."
            };

            await _mediator.Send(request);
            return RedirectToAction("EditQuestion", "Home", new { id = id });
        }

        [Authorize]
        [HttpGet("AddQuestion/{id}")]
        public async Task<IActionResult> AddQuestion([FromRoute] string id)
        {
            return View(new AddQuestionModel()
            {
                PollId = id,
                Text = "Paste your text here ..."
            });
        }

        [Authorize]
        [HttpPost("AddQuestion/{id}")]
        public async Task<IActionResult> AddQuestion([FromRoute] string id, AddQuestionModel model)
        {
            var request = new AddQuestionRequest()
            {
                PollId = id,
                Text = model.Text,
                Type = model.Type,
                Variants = new List<VariantDto>()
            };

            await _mediator.Send(request);

            return RedirectToAction("Edit", "Home", new { id = id });
        }

        [Authorize]
        [HttpGet("AddPoll")]
        public async Task<IActionResult> AddPoll()
        {
            return View(new AddPollModel()
            {
                Name = "Paste poll name here ...",
                Description = "Paste poll description here ...",
            });
        }

        [Authorize]
        [HttpPost("AddPoll")]
        public async Task<IActionResult> AddPoll(AddPollModel model)
        {
            var request = new AddPollRequest()
            {
                UserId = ((UserDto)HttpContext.Items["User"]).Id,
                Name = model.Name,
                Description = model.Description,
                Questions = new List<QuestionDto>()
            };

            var response = await _mediator.Send(request);

            return RedirectToAction("Edit", "Home", new { id = response.PollId });
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel data)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(new SignUpRequest()
                {
                    Name = data.Name,
                    Age = data.Age,
                    Sex = data.Sex,
                    Country = data.Country,
                    Login = data.Login,
                    Password = data.Password,
                });

                return RedirectToAction("Login");
            }
            return View(data);
        }

        [HttpGet("Take/{id}")]
        public async Task<IActionResult> Take([FromRoute] string id)
        {
            var response = await _mediator.Send(new GetPollRequest()
            {
                Id = id
            });

            var model = new TakePollModel()
            {
                PollId = id,
                Name = response.Name,
                Description = response.Description,
                Questions = new List<TakePollQuestionItem>()
            };

            foreach (var i in response.Questions)
            {
                var item = new TakePollQuestionItem()
                {
                    QuestionId = i.QuestionId,
                    Text = i.Text,
                    Type = i.Type,
                    Variants = new List<TakePollVariantItem>()
                };
                foreach (var j in i.Variants)
                {
                    item.Variants.Add(new TakePollVariantItem()
                    {
                        Text = j.Text,
                        VariantId = j.VariantId,
                        IsSelected = false
                    });
                }
                model.Questions.Add(item);
            }

            return View(model);
        }

        [HttpPost("Take/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Take([FromRoute] string id, TakePollModel data)
        {
            if (ModelState.IsValid)
            {
                var request = new SurveyRequest()
                {
                    UserId = ((UserDto)HttpContext.Items["User"]).Id.ToString(),
                    Responses = new List<SurveyItem>()
                };

                foreach (var i in data.Questions)
                {
                    if (i.Type == "Radiobox")
                    {
                        request.Responses.Add(new SurveyItem()
                        {
                            QuestionId = i.QuestionId,
                            Variants = new List<string>() { i.SingleVariantId }
                        });
                    }
                    else
                    {
                        request.Responses.Add(new SurveyItem()
                        {
                            QuestionId = i.QuestionId,
                            Variants = i.Variants.Where(x => x.IsSelected).Select(x => x.VariantId).ToList()
                        });
                    }
                }
                var response = await _mediator.Send(request);

                return RedirectToAction("Main");
            }
            return View(data);
        }

        [HttpGet("Stats/{id}")]
        public async Task<IActionResult> Stats([FromRoute] string id)
        {
            var response = await _mediator.Send(new GetStatsRequest()
            {
                Id = id
            });

            return View(response);
        }
    }
}
