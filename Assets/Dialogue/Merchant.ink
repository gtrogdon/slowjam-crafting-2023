INCLUDE Globals.ink

-> merchant

=== merchant ===
May I interest you in some wares? *beep boop* #speaker:Merchant
 * [Yes] -> buy 
 * [No.] -> nobuy

=== buy ===
Yes please. #speaker:{playerName}
 You won't regret it. *beep boop* #speaker:Merchant
-> DONE
 
=== nobuy ===
No thanks. #speaker:{playerName}
 Ok. Let me know if you change your mind. #speaker:Merchant
-> DONE