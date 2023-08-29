=== search_parts ===
{gaveArm == true: -> theend | -> craft }

=== craft ===
{ armCrafted == true: -> giveArm | -> arm }

=== arm ===
Have you found anything useful? #speaker:Eva
 + {foundParts} [Yes] -> found_parts 
 + {!foundParts} [Still working on it.] -> looking_for_parts

=== found_parts ===
Yes, we'll get you fixed up soon. #speaker:Laura
 [*smiles*] Okay, let's see what you can tinker up. #speaker:Eva
-> DONE

=== looking_for_parts ===
Still working on it.. #speaker:Laura
 Maybe we should accept we can't fix this.. #speaker:Eva
  ... #speaker:Laura
-> DONE

=== giveArm ===
What have you got there? #speaker:Eva
    * {paperArm} [*hand over PaperMacheArm*]
     ... at least it's pretty... #speaker:Eva
      ~ setGaveArmTrue()
      -> DONE
    * {pipeArm} [*hand over Pipe Arm*]
     Can't pick up items with this, but at least it's good for putting up a fight. #speaker:Eva
      ~ setGaveArmTrue()
      -> DONE

=== theend ===
Thanks for everything you've done for me.  #speaker:Eva
-> END

=== function setGaveArmTrue() ===  
~ gaveArm = true

