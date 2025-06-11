using Dotel2.DTOs;
using Dotel2.Models;

namespace Dotel2.Repository.User
{
    public interface IUserRepository
    {
        public Models.User getUserbyRentalId(int uId);
        public bool checkUserMemberShip(Models.User user);

        public void createNewConvesation(Conversations conversations);
        public Conversations getConversationByUserId(int userIdFrom, int userIdTo);
        public ConversationDTO GetConversation(int conversationId, int currentUserId);

        public List<ConversationDTO> getConversationsByUserId(int userId);
        
    }
}
