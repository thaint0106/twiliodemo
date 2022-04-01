using System;
using System.Collections.Generic;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Twilio;
using Twilio.Jwt.AccessToken;
using Twilio.Rest.Conversations.V1;
using Twilio.Rest.Conversations.V1.User;
using conversition = Twilio.Rest.Conversations.V1.Conversation;
namespace Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {

        // requires using Microsoft.Extensions.Configuration;
        private readonly IConfiguration configuration;
        private readonly string accountSid;
        private readonly string authToken;
        private readonly string serviceId;
        private readonly string twilioApiKey;
        private readonly string twilioApiSecret;

        public ChatController(IConfiguration configuration)
        {
            this.configuration = configuration;
            accountSid = configuration["TWILIO_ACCOUNT_SID"];
            authToken = configuration["TWILIO_AUTH_TOKEN"];
            this.serviceId = "MG7df4939dd422781d009e9047daacb57e";
            twilioApiKey = "SK0cc4b45041fd523472cb8f705b7060c7";
            twilioApiSecret = "MadqTQviChWfzvSH1GhfAQ2IXLAopEJh";
        }

        [HttpGet]
        [Route("chat-token")]
        public string GetToken(string userResourceId)
        {
            TwilioClient.Init(accountSid, authToken); // Create an Chat grant for this token
            try
            {
                var user = UserResource.Fetch(pathSid: userResourceId);
                var grant = new ChatGrant
                {
                    ServiceSid = user.ChatServiceSid
                }; var grants = new HashSet<IGrant> { { grant } };
                // Create an Access Token generator
                var token = new Token(
                accountSid,
                twilioApiKey,
                twilioApiSecret,
                user.Identity,
                grants: grants);
                return token.ToJwt();
            }
            catch (Exception e)
            {
                return "";
            }
        }

        [HttpGet]
        public IEnumerable<ConversationResource> Get()
        {
            TwilioClient.Init(accountSid, authToken);
            var conversations = ConversationResource.Read(limit: 20);
            return conversations;
        }


        [HttpGet]
        [Route("user-conversition")]
        public IEnumerable<UserConversationResource> GetUserConversition(string id)
        {
            TwilioClient.Init(accountSid, authToken);
            var userConversations = UserConversationResource.Read(
            pathUserSid: id,
            limit: 20
        );
            return userConversations;
        }

        [HttpPost]
        [Route("add-user-conversition")]
        public conversition.ParticipantResource AddUserConversition(UserChannel model)
        {
            TwilioClient.Init(accountSid, authToken);
            var participant = conversition.ParticipantResource.Create(
              identity: model.Email,
              pathConversationSid: model.ConversitionId
            );
            return participant;
        }

       

        [HttpPost]
        [Route("send-message")]
        public bool SendMessgae(Message model)
        {
            TwilioClient.Init(accountSid, authToken);
            var message = Twilio.Rest.Conversations.V1.Conversation.MessageResource.Create(
           author: model.Author,
           body: model.Content,
           pathConversationSid: model.ConversitionId
            );
            return true;
        }

        [HttpPost]
        [Route("create-conversition")]
        public bool CreateConversition(Channel model)
        {
            TwilioClient.Init(accountSid, authToken);
            var conversation = ConversationResource.Create(
              messagingServiceSid: serviceId,
              friendlyName: model.Name
            );

            return true;
        }

        [HttpPost]
        [Route("create-user")]
        public bool CreateUserResource(UserChannel model)
        {
            TwilioClient.Init(accountSid, authToken);
            var user = UserResource.Create(identity: model.Email);
            return true;
        }
        
        [HttpPost]
        [Route("create-participant")]
        public bool AddParticipant(UserChannel model)
        {
            var participant = Twilio.Rest.Conversations.V1.Conversation.ParticipantResource.Create(
           identity: "NTA_57f14c90-a76c-40a1-9ac2-9b0b2d308407",
           pathConversationSid: "CH2a27df0979694215b4c9dcc88a39d0bd"
       );
            return true;
        }

        [HttpPost]
        [Route("create-user-conversition")]
        public bool CreateUserConversition(UserChannel model)
        {
            TwilioClient.Init(accountSid, authToken);
            var userConversation = UserConversationResource.Fetch(
            pathUserSid: model.UserId,
            pathConversationSid: model.ConversitionId
        );

            return true;
        }


        [HttpPost]
        [Route("create-service")]
        public bool CreateService(Channel model)
        {
            TwilioClient.Init(accountSid, authToken);
            var service = Twilio.Rest.Messaging.V1.ServiceResource.Create(friendlyName: model.Name);
            return true;
        }

        [HttpDelete]
        [Route("delete-conversition")]
        public bool DeleteConversition(string id)
        {
            TwilioClient.Init(accountSid, authToken);
            ConversationResource.Delete(pathSid: id);
            return true;
        } 
        [HttpDelete]
        [Route("delete-user")]
        public bool DeleteUser(string id)
        {
            TwilioClient.Init(accountSid, authToken);
         // Twilio.Rest.Conversations.V1.Conversation.ParticipantResource.Delete(
         //    pathConversationSid: "CH2a27df0979694215b4c9dcc88a39d0bd",
         //    pathSid: "MB12c1485b382c40fca1ed12feafdc9c9e"
         //); 
            Twilio.Rest.Conversations.V1.Conversation.ParticipantResource.Delete(
             pathConversationSid: "CH2a27df0979694215b4c9dcc88a39d0bd",
             pathSid: id
         );
            return true;
        }

        [HttpGet]
        [Route("guid")]
        public Guid GenGuid(string id)
        {
            return Guid.NewGuid();

        }
    }
}
