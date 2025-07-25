using BotSharp.Abstraction.Hooks;

namespace BotSharp.Abstraction.Conversations;

public interface IConversationHook : IHookBase
{
    int Priority { get; }
    Agent Agent { get; }
    List<RoleDialogModel> Dialogs { get; }
    IConversationHook SetAgent(Agent agent);

    Conversation Conversation { get; }
    IConversationHook SetConversation(Conversation conversation);

    /// <summary>
    /// Get the predifined intent for the conversation.
    /// It will send to the conversation context to help LLM to understand the user's intent.
    /// </summary>
    /// <returns></returns>
    Task<string> GetConversationIntent() => Task.FromResult(string.Empty);

    /// <summary>
    /// Triggered when user connects with agent first time.
    /// This hook is the good timing to show welcome infomation.
    /// </summary>
    /// <param name="conversation"></param>
    /// <returns></returns>
    Task OnUserAgentConnectedInitially(Conversation conversation);

    /// <summary>
    /// Triggered once for every new conversation.
    /// </summary>
    /// <param name="conversation"></param>
    /// <returns></returns>
    Task OnConversationInitialized(Conversation conversation);

    /// <summary>
    /// Triggered when dialog history is loaded.
    /// </summary>
    /// <param name="dialogs"></param>
    /// <returns></returns>
    Task OnDialogsLoaded(List<RoleDialogModel> dialogs);

    /// <summary>
    /// Triggered when every dialog record is loaded
    /// It can be used to populate extra data point before presenting to user.
    /// </summary>
    /// <param name="dialog"></param>
    /// <returns></returns>
    Task OnDialogRecordLoaded(RoleDialogModel dialog);

    Task OnStateLoaded(ConversationState state);
    Task OnStateChanged(StateChangeModel stateChange);

    Task OnMessageReceived(RoleDialogModel message);
    Task OnPostbackMessageReceived(RoleDialogModel message, PostbackMessageModel replyMsg);

    /// <summary>
    /// Triggered before LLM calls function.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="from"></param>
    /// <returns></returns>
    Task OnFunctionExecuting(RoleDialogModel message, string from = InvokeSource.Manual);

    /// <summary>
    /// Triggered when the function calling completed.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="from"></param>
    /// <returns></returns>
    Task OnFunctionExecuted(RoleDialogModel message, string from = InvokeSource.Manual);

    Task OnResponseGenerated(RoleDialogModel message);

    /// <summary>
    /// LLM detected the whole conversation is going to be end.
    /// </summary>
    /// <param name="conversation"></param>
    /// <returns></returns>
    Task OnConversationEnding(RoleDialogModel message);

    /// <summary>
    /// LLM can't handle user's request or user requests human being to involve.
    /// </summary>
    /// <param name="conversation"></param>
    /// <returns></returns>
    Task OnHumanInterventionNeeded(RoleDialogModel message);

    /// <summary>
    /// Delete message in a conversation
    /// </summary>
    /// <param name="conversationId"></param>
    /// <param name="messageId"></param>
    /// <returns></returns>
    Task OnMessageDeleted(string conversationId, string messageId);

    /// <summary>
    /// Brakpoint updated
    /// </summary>
    /// <param name="conversationId"></param>
    /// <returns></returns>
    Task OnBreakpointUpdated(string conversationId, bool resetStates);

    /// <summary>
    /// Generate a notification
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    Task OnNotificationGenerated(RoleDialogModel message);
}
