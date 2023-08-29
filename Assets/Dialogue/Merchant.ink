INCLUDE Globals.ink
EXTERNAL openShopUI()

-> merchant

=== merchant ===
May I interest you in some wares? *beep boop* #speaker:Scoop
 * [Yes] -> buy 
 * [No.] -> nobuy

=== buy ===
Yes please. #speaker:Laura
 ~ openShopUI()
-> DONE
 
=== nobuy ===
No thanks. #speaker:{playerName}
 Ok. Let me know if you change your mind. #speaker:Scoop
-> DONE