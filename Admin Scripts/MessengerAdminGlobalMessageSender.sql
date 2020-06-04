INSERT INTO Messages
(Messages.Message_SenderID, Message_ReceiverID, Message_Content, Message_DateTime, Message_Status, Message_Trace)
SELECT 1000, Members_ID, '<u><b>Announcement #1:</b></u> <i>CONTENT</i> - <b>@Administrator<b>', FORMAT ( GETDATE(), 'yyyy:MM:dd - HH:mm:ss:ff') , 'unread', 'N/A' From Members WHERE Members_ID != 1000