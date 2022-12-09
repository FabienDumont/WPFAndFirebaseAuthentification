using System.Threading.Tasks;
using Refit;
using WPFAndFirebaseAuthentification.Core.Responses;

namespace WPFAndFirebaseAuthentification.WPF.Queries; 

public interface IGetMessageQuery {
    [Get("/")]
    Task<MessageResponse> Execute();
}