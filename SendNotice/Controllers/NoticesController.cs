using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SendNotice.Data;
using SendNotice.Models;
using AutoMapper;
using SendNotice.Dtos;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Microsoft.Extensions.Configuration;

namespace SendNotice.Controllers
{
    [Route("api/notices")]
    [ApiController]
    public class NoticesController : ControllerBase
    {

        private readonly ISendNoticeRepo _repository;
        private readonly IMapper _mapper;
        public IConfiguration Configuration { get; }

        public NoticesController(ISendNoticeRepo repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            Configuration = configuration;

            TwilioClient.Init(Configuration.GetSection("accountSid").ToString(), Configuration.GetSection("authToken").ToString());
        }

        [HttpGet]
        public ActionResult<IEnumerable<NoticeReadDto>> GetAllNotices()
        {
            var noticeItems = _repository.GetNotices();
            return Ok(_mapper.Map<IEnumerable<NoticeReadDto>>(noticeItems));
        }


        [HttpGet("{id}", Name = "GetNoticeById")]
        public ActionResult<NoticeReadDto> GetNoticeById(int id)
        {
            var noticeItem = _repository.GetNoticeById(id);
            if (noticeItem != null)
            {
                return Ok(_mapper.Map<NoticeReadDto>(noticeItem));
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<NoticeReadDto> CreateNotice(NoticeCreateDto noticeCreateDto)
        {
            var noticeModel = _mapper.Map<Notice>(noticeCreateDto);
            _repository.CreateNotice(noticeModel);
            _repository.SaveChanges();

            var noticeReadDto = _mapper.Map<NoticeReadDto>(noticeModel);
            CreatedAtRoute(nameof(GetNoticeById), new { Id = noticeReadDto.Id }, noticeReadDto);

            var message = MessageResource.Create(body: noticeReadDto.Message,
               from: new Twilio.Types.PhoneNumber(Configuration.GetSection("senderid").ToString()),
               to: new Twilio.Types.PhoneNumber(noticeReadDto.Phone)
           );

            return Ok(noticeReadDto);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteNotice(int id)
        {
            var noticeModel = _repository.GetNoticeById(id);
            if (noticeModel == null)
            {
                return NotFound();
            }
            _repository.DeleteNotice(noticeModel);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}