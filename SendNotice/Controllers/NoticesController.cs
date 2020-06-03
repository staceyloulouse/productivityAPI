using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SendNotice.Data;
using SendNotice.Models;
using AutoMapper;
using SendNotice.Dtos;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace SendNotice.Controllers
{
    [Route("api/notices")]
    [ApiController]
    public class NoticesController : ControllerBase
    {

        private readonly ISendNoticeRepo _repository;
        private readonly IMapper _mapper;

        public NoticesController(ISendNoticeRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

            const string accountSid = "AC58f4635b412fea7d8febc46c3f04bb19";
            const string authToken = "632a1a46b5d89410efa03a16395e6db1";

            TwilioClient.Init(accountSid, authToken);
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

            

            var message = MessageResource.Create(
                body: "Hey, Welcome to our new SMS platform. From KIBET",
                from: new Twilio.Types.PhoneNumber("+18506008350"),
                to: new Twilio.Types.PhoneNumber("+254719453783")
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