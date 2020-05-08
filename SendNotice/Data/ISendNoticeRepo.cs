using System.Collections;
using System.Collections.Generic;
using SendNotice.Models;

namespace SendNotice.Data
{
    public interface ISendNoticeRepo
    {
        bool SaveChanges();
        IEnumerable<Notice> GetNotices();
        Notice GetNoticeById(int id);
        IEnumerable<Unit> GetUnits();
        Unit GetUnitById(int id);
        void InsertUnit(Unit unit);
        void CreateNotice(Notice note);
        void UpdateUnit(Unit unit);
        void DeleteNotice(Notice note);
        Notice SendNoticeToUnit(int id, string message);

        
        
    }
}