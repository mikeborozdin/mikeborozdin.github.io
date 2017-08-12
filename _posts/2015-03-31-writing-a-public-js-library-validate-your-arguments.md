---
layout: post
title: "Writing a JavaScript Public Library? Validate Your Arguments"
comments: true
---

How many times have seen an obscure JavaScript error when using one 3rd party library or another? More likely than not, you’ve come across a message like:

	TypeError: Cannot call method 'someMethod' of undefined

Your browser or the Node.js interpreter will tell you in which file and on which line that error has occurred. Then, you’ll start a very mundane task of examining the code of a 3rd party library, if it’s has not been minimised, of course. Eventually, you will find out that you have forgotten to provide one of the arguments required by a function.
You may even see a more obscure error. <!-- more --> Let’s take the standard Node.js library for an example, more specifically, its `http` module. It has the `request()` function that sends an HTTP request that can be meticulously configured with an object you pass into. What if you accidentally confuse one of the object’s attributes, for example, instead of typing `hostname` you write `url`, e.g.

{% highlight javascript lineanchors %}
var http = require('http');

var req = http.request({
 url: 'www.google.com'
});
{% endhighlight %}

Then, you will see an error saying exactly the following:

	throw er; // Unhandled 'error' event
	Error: connect ECONNREFUSED

Urgh, what could it mean? Has Google suddenly gone down? No, you should have read the documentation more carefully and remember that the attribute is called `hostname`, not `url`.

Okay, so, where does the problem lie? Is it a developer’s problem that they have accidentally made mistake and spent hours debugging? Let’s face, we all make mistakes. Some tools though provide better feedback than the other ones.

Can Static Typing Really Help?
----

It might sound that I'm arriving on the idea of introducing static typing in JavaScript - this is not true. However, I believe that when we're writing code in JavaScript we still assume certain types. In other words, if we create a multiplying function, like the one below:

{% highlight javascript lineanchors %}
function multiply(a, b) {
	return a * b;
}
{% endhighlight %}

We presume that `a` and `b` are numbers, not strings or objects. This idea is beautifully put by Facebook [in their introduction to Flow](https://code.facebook.com/posts/1505962329687926/flow-a-new-static-type-checker-for-javascript/), a static type checking for JavaScript - _'the design of Flow is the assumption that most JavaScript code is implicitly statically typed; even though types may not appear anywhere in the code, they are in the developer’s mind as a way to reason about the correctness of the code'_.

Would even having static typing in JavaScript help? After all, you can already use TypeScript...

Well, partially... Statically typed languages are still prone to similar issues. Let's take Java as an example. It can provide great feedback to a developer during a compilation. What about the run time though? How many time have you seen a `NullPointerException` creeping out of nowhere?

Is Java's `NullPointerException` anyhow different from the problem with `undefined` and `null` in JavaScript? Not really. Either way, you'll have to analyse a call stack.

Consider the following example:

{% highlight java lineanchors %}
public class CustomerPrinter {
 public void printCustomerInfo(final Customer customer) {
   System.out.println(customer.getName());
 }
}
{% endhighlight %}

The code is written in a statically type language, so you cannot pass an `int` or a `String`. What about passing a `null` though?

{% highlight java lineanchors %}
new CustomerPrinter().printCustomerInfo(null);
{% endhighlight %}

That problem will not be caught when compiling the code, instead there will be a runtime exception. The code snippet above is fairly simple. Imagine calling `customer.getName()` somewhere deep inside the method? That would be 'fun' to debug...

It’s hardly a surprise that even Java, a statically typed language, has libraries, such as Guava, one of which goals is to solve the problem by [failing fast](http://stackoverflow.com/questions/26184322/whats-the-point-of-guava-checknotnull).

Developers, using Guava, would strive to use `Preconditions` to fail as early as possibly and provide meaningful information if something goes wrong, e.g.:

{% highlight java lineanchors %}
public class CustomerPrinter {
 public void printCustomerInfo(final Customer customer) {
   Preconditions.checkNotNull(customer);

   System.out.println(customer.getName());
 }
}
{% endhighlight %}

Fail Fast
----

Here, we arrive on a very important point when it comes to the API design. Do fail fast. Please, do. I also beg you to provide an informative message to a user. We all use 3rd party libraries because they solve problems we don’t want to specialise in. And we don’t want to dive into the code of those libraries to find out why we see that message about some undefined value or something even more esoteric.

All we want to see is a nice message saying that a certain argument is not what a function expects. That can be implemented with a fairly simple piece of code:

{% highlight javascript lineanchors %}
function sendHttpRequest(params) {
  if (typeof params !== 'object' || typeof params.hostname !== 'string') {
    throw new TypeError('Param must be an object that has the `hostname` attribute');
  }

  //do something useful
}
{% endhighlight %}
