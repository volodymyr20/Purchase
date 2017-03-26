# Purchase application

Please refer to [Shopping.cs](https://github.com/volodymyr20/Purchase/blob/master/Purchase/Shopping.cs) for the description of what application is about.

Sample input data: 

**John's receipt:**

```
Item 	Qty		Price		Subtotal
Milk 	1 		10 		10
Water	9		8*4+1*6		38

(no discount pairs)

Total: 48
```

**Bill's receipt:**

```
Item 	Qty		Price		Subtotal
Sugar 	1 		15		15
Water 	10		8*4+2*6		44
Bread 	1 		9		9 

Discount pairs: 
Sugar, Water: 10 
Sugar, Bread 15 
 
(15+44)*0.1 + 9*0.15 = 7.25

Total: 60.75
```

Expected program output: 

![alt tag](https://github.com/volodymyr20/Purchase/blob/master/output.png)

