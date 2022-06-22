using System.Threading.Tasks;

namespace KT.Turnstile.Unit.ClientApp.Dao.Base
{
    public class EntitySeed
    {
        public static Task ForLoginUserAsync(TurnstileUnitContext context)
        {
            return Task.CompletedTask;
        }
    }
}
