using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.EntitySet
{
    public class Student
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
        public IList<ESignDocumentPack> ESignDocumentPacks { get; set; }
        public IList<ESignWebhookLog> ESignWebhookLogs { get; set; }
        public IList<ESignWebhookSubscription> EsignWebhookSubscriptions { get; set; }
    }
}
