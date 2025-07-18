using Volo.Abp.Settings;

namespace FileDock.Domain.Shared.Settings
{
    internal class TestSettingProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            context.Add(
             new SettingDefinition("DDD","127.0.0.1"),
             new SettingDefinition("Test", null)
         );


        }
    }
}
