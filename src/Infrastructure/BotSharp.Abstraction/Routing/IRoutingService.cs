namespace BotSharp.Abstraction.Routing;

public interface IRoutingService
{
    Agent Router { get; }
    IRoutingContext Context { get; }

    /// <summary>
    /// Get routable agents
    /// </summary>
    /// <param name="profiles">router's profile</param>
    /// <returns></returns>
    RoutableAgent[] GetRoutableAgents(List<string> profiles);

    /// <summary>
    /// Get rules by agent name
    /// </summary>
    /// <param name="name">agent name</param>
    /// <returns></returns>
    RoutingRule[] GetRulesByAgentName(string name);

    /// <summary>
    /// Get rules by agent id
    /// </summary>
    /// <param name="id">agent id </param>
    /// <returns></returns>
    RoutingRule[] GetRulesByAgentId(string id);

    //void ResetRecursiveCounter();
    //int GetRecursiveCounter();
    //void SetRecursiveCounter(int counter);

    Task<bool> InvokeAgent(string agentId, List<RoleDialogModel> dialogs, string from = InvokeSource.Manual);
    Task<bool> InvokeFunction(string name, RoleDialogModel messages, string from = InvokeSource.Manual);
    Task<RoleDialogModel> InstructLoop(Agent agent, RoleDialogModel message, List<RoleDialogModel> dialogs);

    /// <summary>
    /// Talk to a specific Agent directly, bypassing the Router
    /// </summary>
    /// <param name="agent"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    Task<RoleDialogModel> InstructDirect(Agent agent, RoleDialogModel message, List<RoleDialogModel> dialogs);

    Task<string> GetConversationContent(List<RoleDialogModel> dialogs, int maxDialogCount = 100);

    (bool, string) HasMissingRequiredField(RoleDialogModel message, out string agentId);
}
