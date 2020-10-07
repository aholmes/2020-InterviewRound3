# Theory
> Question: if only some of the files in the customer purchase database are corrupt, how would you address this problem going forward? What if the database was extremely large? How do you prepare for future data corruptions?Write a brief summary of your approach.

Fortunately, I read the entire project requirement before starting. As such, I started with a unit test that tested these problems, and I developed the application with these questions in mind. These problems are solved, and the unit tests now pass.

Let me address each question in order.

> If only some of the files in the customer purchase database are corrupt, how would you address this problem going forward?

This concern is moot. The application begins with importing purchase data and validates against the corrupted data scenario (where Product Types can be wrong by a single character). This validation _does_ require additional cycles and memory to complete, but in most cases should not be a problem.

My approach: always validate; never assume the data is correct.

One issue that may arrise is with huge data sets, but in reality, how many customers will purchase the excessive number of products that would be necessary to significantly slow down a BK Tree? Taking some measurements, this list of [370k words](https://raw.githubusercontent.com/dwyl/english-words/7cb484d/words_alpha.txt) is indexed in ~20 seconds, and can search a worst-case scenario, e.g., "zwitterionic" in ~100ms.

This approach is not a concern across multiple purchases either if the system can be parallelized. More than one purchase can be analyzed at a time, reducing the total processing time further.

> What if the database was extremely large?

Generally, I would start with considering what is actually possible or likely. As I started in the previous question, it is unlikely customers will purchase an unbelievable number of products. This means the use of an efficient text searching data structure and algorithm, like a BK Tree, should never become a bottle neck. Hopefully the bottle neck is I/O (network, disk, etc).

If we are processing purchase data across many sets of purchases, this could realistically be problematic. If we were, say, analyzing every purchase made at Amazon for its entire existence, that is a lot of data! In this case, I would rely on parallelization first. Multiple cores, multiple machines, or multiple cloud processing instances can give you the raw resources required to churn through so much data. If this is still insufficient, then it's time to start looking at different algorithms, or perhaps using statistics and probabilities, and whether 100% accurate numbers are needed.

> How do you prepare for future data corruptions?

Future data corruptions are handled the same - the application supports this from the start. Now, if we are processing data that we have already processed _because it is now corrupt_, then we need to devise a mechanism to relate the corrupted data to the corrected data. In some cases, reprocessing like that may be best to just start anew.

If we can't do that, having implemented some infrastructure that can help us manage this data would be a good idea. For example, an SNS topic could be used to respond immediately to a Product ID change in a purchase, which can then be validated and reinserted, and you could receive notifications that data is becoming corrupt and stop it before it gets out of hand.