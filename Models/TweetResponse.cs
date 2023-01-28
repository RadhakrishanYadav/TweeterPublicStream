using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TweetResponse
    {
        public double TotalTweetsCount { get; set; }

        public double AverageTweetsPerMinute { get; set; }

        public MessageStatusModel? ErrorModel { get; set; }
    }
}
