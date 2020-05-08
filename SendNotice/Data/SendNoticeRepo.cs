using System;
using System.Data;
using System.Collections.Generic;
using SendNotice.Models;
using System.Linq;

namespace SendNotice.Data
{
    public class SendNoticeRepo : ISendNoticeRepo
    {
        private readonly SendNoticeContext _context;

        public SendNoticeRepo(SendNoticeContext context)
        {
            _context = context;
        }

        public void CreateNotice(Notice note)
        {
            if (note == null)
            {
                throw new ArgumentNullException(nameof(note));
            }
            note.TimeStamp = DateTime.Now;
            _context.Notices.Add(note);
        }

        public void DeleteNotice(Notice note)
        {
            if(note == null)
            {
                throw new ArgumentNullException(nameof(note));
            }

            _context.Notices.Remove(note);
        }

        public Notice GetNoticeById(int id)
        {
            return _context.Notices.FirstOrDefault(p=> p.Id == id);
        }

        public IEnumerable<Notice> GetNotices()
        {
            
            return _context.Notices.ToList();
        }

        public Unit GetUnitById(int id)
        {
            return _context.Units.FirstOrDefault(p=> p.Id == id);
        }

        public IEnumerable<Unit> GetUnits()
        {
            
            return _context.Units.ToList();;
        }

        public void InsertUnit(Unit unit)
        {
            if (unit == null)
            {
                throw new ArgumentNullException(nameof(unit));
            }
            _context.Units.Add(unit);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public Notice SendNoticeToUnit(int id, string message)
        {
            //consume smsgateway api - infobip.com
            return new Notice {Id=id, TimeStamp = System.DateTime.Now , Message = message, Phone = "8034143379" };
        }

        public void UpdateUnit(Unit unit)
        {
            //nothing needs to be done
        }
    }
}