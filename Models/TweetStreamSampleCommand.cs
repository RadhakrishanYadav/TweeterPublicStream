using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TweetStreamSampleCommand : IRequest<bool>
    {
        public string? Id { get; set; }
        public string? Text { get; set; }
    }
}
