using System.Collections.Generic;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Rest.Chat.V1;
using Twilio.Rest.Chat.V1.Service;
using Twilio.Rest.Chat.V1.Service.Channel;
using Twilio.Rest.Chat.V1.Service.User;

namespace Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TwilioController : ControllerBase
    {

        // requires using Microsoft.Extensions.Configuration;
        private readonly IConfiguration configuration;
        private readonly string accountSid;
        private readonly string authToken;
        private readonly string serviceId;
        public TwilioController(IConfiguration configuration)
        {
            this.configuration = configuration;
            accountSid = configuration["TWILIO_ACCOUNT_SID"];
            authToken = configuration["TWILIO_AUTH_TOKEN"];
            this.serviceId = "IS019d4cea10144cdbafa7d8a1484bd34e";

        }

        [HttpGet]
        public IEnumerable<ChannelResource> Get()
        {
            TwilioClient.Init(accountSid, authToken);
            var channels = ChannelResource.Read(
            pathServiceSid: serviceId,
            limit: 20
        );
            return channels;
        }

        [HttpGet]
        [Route("user-resource")]
        public UserResource GetUserResoure(string id)
        {
            var user = UserResource.Fetch(
             pathServiceSid: serviceId,
             pathSid: id
          );
            return user;
        }

        [HttpGet]
        [Route("message")]
        public IEnumerable<MessageResource> GetMessage(string id)
        {
            var messages = MessageResource.Read(
            pathServiceSid: serviceId,
            pathChannelSid: id,
            limit: 20
        );

            return messages;
        }

        [HttpGet]
        [Route("user-channel")]
        public IEnumerable<MemberResource> GetUserChannel (string id)
        {
            var members = MemberResource.Read(
             pathServiceSid:serviceId,
             pathChannelSid: id,
             limit: 20
         );
            return members;
        }

        [HttpPost]
        [Route("create-message")]
        public bool CreateService(Message message)
        {
            var result = MessageResource.Create(
                body:message.Content,
                pathServiceSid: serviceId,
                pathChannelSid:message.ChannelId,
                from: message.From
            );
            return true;
        }


        [HttpPost]
        [Route("create-service")]
        public bool CreateService()
        {

            TwilioClient.Init(accountSid, authToken);
            var service = ServiceResource.Create(friendlyName: "nta chat demo");
            return true;
        }

        [HttpPost]
        [Route("create-user")]
        public bool CreateUserResource(UserChannel model)
        {
            TwilioClient.Init(accountSid, authToken);
            var user = UserResource.Create(
             identity: model.Email,
             pathServiceSid: serviceId
            );
            return true;
        }

        [HttpPost]
        [Route("create-user-channel")]
        public bool CreateUserChannelResource(UserChannel model)
        {
            TwilioClient.Init(accountSid, authToken);
            var member = MemberResource.Create(
             identity: model.Email,
             pathServiceSid: serviceId,
             pathChannelSid: model.ChannelId
         );
            return true;
        }



        [HttpPost]
        public bool Post(Channel model)
        {
            TwilioClient.Init(accountSid, authToken);
            var channel = ChannelResource.Create(
            pathServiceSid: serviceId,
            friendlyName: model.Name
            );
            return false;
        }


        [HttpPut]
        public ChannelResource Put(Channel channel)
        {
            TwilioClient.Init(accountSid, authToken);

            var result = ChannelResource.Update(
                    friendlyName: channel.Name,
                    pathServiceSid: channel.ServiceId,
                    pathSid: channel.Id
                );
            return result;
        }

        [HttpDelete]
        public bool Delete(string id)
        {
            TwilioClient.Init(accountSid, authToken);
            return ChannelResource.Delete(
           pathServiceSid: serviceId,
           pathSid: id
           );
        }

        [HttpDelete]
        [Route("delete-service")]
        public bool DeleteService(string id)
        {
            TwilioClient.Init(accountSid, authToken);
            return ServiceResource.Delete(pathSid: id);
        }
    }
}
