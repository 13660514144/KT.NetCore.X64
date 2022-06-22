using System.Threading.Tasks;

namespace KT.Elevator.Unit.Processor.ClientApp.Dao.Base
{
    public class EntitySeed
    {
        public static Task ForLoginUserAsync(ElevatorUnitDbContext context)
        {
            return Task.CompletedTask;
        }
    }
}
