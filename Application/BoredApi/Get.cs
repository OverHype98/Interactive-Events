using Application.Activities;
using Application.Core;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.BoredApi
{
	public class Get
	{
        public class Query : IRequest<Result<BoredApiDto>>
        {

        }

        public class Handler : IRequestHandler<Query, Result<BoredApiDto>>
        {

			public string Link { get => "http://www.boredapi.com/api/activity"; }
            public Handler(IMapper mapper)
            {
	
			}

            public async Task<Result<BoredApiDto>> Handle(Query request, CancellationToken cancellationToken)
            {

				HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(Link);



				HttpWebResponse response = (HttpWebResponse)httpRequest.GetResponse();
				Stream dataStream = response.GetResponseStream();
				StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                var activity = JsonConvert.DeserializeObject<BoredApiDto>(responseFromServer);

                if (activity != null) Result<BoredApiDto>.Failure("Failed to retrieve from bored api");

                return Result<BoredApiDto>.Success(activity);
            }
        }
    }
}

