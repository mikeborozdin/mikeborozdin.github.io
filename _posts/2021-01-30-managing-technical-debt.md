---
layout: post
title: "Managing technical debt"
comments: true
---

Often engineering teams struggle with technical debt. They are busy with building features and fixing bugs, but that’s not going well due to a huge amount of technical debt. However, they hardly get any time to fix any of the debt.

In this article I’m going to share my experience on how to manage technical debt and get it fixed.<!-- more -->

# What is technical debt?

First of all, what is technical debt? 

Technical debt is something that slows us down. Technical debt is a code that you are afraid to touch and if you do you produce multiple bugs. Technical debt is a messy code that does not let you ship features fast. Technical debt is an obsolete library version that doesn't allow us to use the latest techniques.

Scalability issues though are not technical debt (more on that below).

# Why do teams struggle to address technical debt?

In my experience, it happens simply because it nevers gets prioritised. And that’s usually due to the fact that software developers tend to speak of technical debt purely from a technical point of view. And a result, product managers, who drive prioritisation, don’t quite get the value of it - and that’s why work on fixing technical debt always ends up in the bottom of the backlog.

# Technical debt has business impact

But technical debt does have business impact. Technical debt means that new features will take 2x, 3x, 10x times, as longer, if it were not for technical debt. Technical debt means you’ll dedicate a lot of time fixing bugs, instead of building new features.

Technical debt, like financial debt, means that you’ll spend your disposable income (dev time) paying off interest (fixing long outstanding technical debt) instead of spending it on fun things (new features that customers love).

So when persuading product managers to prioritise technical debt, do talk about the business impact it’ll bring. And talk about the negative effect if you postpone it. Ultimately, it is all about reducing time-to-market (time to build new features) and improving product quality (fewer bugs).

# How to prioritise technical debt?

And speaking of prioritisation, I have observed two strategies:

* Allocate a certain amount of time to technical debt. Say, a day a week
* Prioritise technical debt along with features and bug fixes

Personally, I’m not a fan of the first one. Allocating a certain amount of time seems too arbitrary. There are occasions when addressing technical debt takes more a day a week. Sometimes there are times that you have nothing to address. Plus, that may also seem as a blackbox to non-technical stakeholders. Unless they explicitly ask engineers they won’t know what technical debt is being addressed and how that benefits the product.

# Prioritise technical debt along with features and bug fixes

Instead I prefer to prioritise technical debt along with features and bug fixes. No matter which software development methodology you use, chances are you have some sort of a backlog - a prioritised list of features and bug fixes. So just put technical debt tickets to that list and prioritise them accordingly.

This method has the following advantages:

* It forces you to think of technical debt as atomic pieces of work
* It is tied to the roadmap (more on that below)
* It gives visibility to non-technical stakeholders

I want to emphasise the second point - 'it is tied to the roadmap'. If you see on the roadmap that three new features will touch an ugly part of the code, then you can accelerate development by prioritising refactoring ahead of the feature work. 

# Sometimes you can just inflate time estimates

In the previous example, there are three features which depend on fixing technical debt. And it makes sense to prioritise technical debt separately. Multiple developers will work on those features, you don’t want them to depend on each other. So refactor first and let them build those features after.

But if the roadmap says that there’ll be only one feature that requires touching that scary piece of code, then you don’t even need to schedule any additional work. Just inform a product manager that  a feature will take longer because you need to tidy up the code first. Otherwise (without refactoring), the work will take even more time and there’ll be a lot of bugs after. 

# Old dependency versions can become technical debt

Whatever you’re building probably depends on dozens of 3rd party libraries. And they tend to get new versions over time. 
You need to keep an eye on new versions and prioritise upgrades.

Why?

* New versions can make you more productive. They may introduce new features that simplify your development. And if you don’t upgrade soon enough, your code will start to look obsolete. The dev community will eventually embrace new features of a language or a framework, but you will be still stuck with an old version and its old ways.
* The longer you stay on an older version the more painful you’re making an upgrade. If you are one major version behind an upgrade will probably be smooth-ish, but if you are two, three, four major versions behind, it’s likely to be much more painful
* Support and security vulnerabilities - you don’t want to be using a framework that is no longer supported, do you?

# You can even refactor code as you go along

Nothing prevents you from improving the code quality while you’re fixing bugs or building new features. 

Think that something can be refactored into a separate module? Do it!

Noticed that 3rd party library, you’re calling, is 3 major versions behind? Upgrade it!

But... be reasonable and avoid rabbit holes. 

Will your new feature make a mess out of the existing code? Yes? Then, take your time and do it properly.

Have you noticed that the function you're calling is difficult to understand, but you don’t have to change it? Just write a ticket about it and prioritise it separately.

When making such decisions also think of people who’ll be reviewing your code. The more things you’re putting in your pull request, the more time it’ll take for them to review and the more things they can miss in a review.

# Scalability issues are not technical debt

If your website is meant to serve 10,000 customers a minute, but is only capable of doing 500, then it’s not technical debt, it’s a very big gap in your product functionality. It’s the same thing as building a payment feature that only accepts Visa cards, but not MasterCard.

Yes, there might be the same cause for technical debt and scalability issues, for instance, you cut corners somewhere. But their impact is very different. As mentioned above technical debt does have a huge business impact, but the downfall from scalability issues is far bigger.

And if you need to get work prioritised, you need to clearly explain why it matters. So if you know that next month you’ll be serving more customers that your app is capable of, then this precisely what you should tell all non-technical stakeholders.

Another way to look at it - if you pause entire development, then technical debt will not be relevant - it won’t slow down anything, as there’ll be no features to build. But if people still use the product, well, they try and they cannot, because your system cannot cope with the demand, then it’s a gap in functionality.

# Finally, build good relationship with product managers

[I have written about it](/post/being-effective-tech-lead/), the other people [have written about it before](https://domk.website/blog/2021-01-12-tech-lead-empowered-product-team.html). It is vital to have a good relationship between product and engineering. Both of you need to have trust. That’s because you’ll have to ask a product manager to pause feature/bug delivery and spend time on technical debt. They, in turn, need to trust you that that time will be well spent and ultimately will benefit users.

# Related posts

* [On being effective tech lead](/post/being-effective-tech-lead/)
* [Split User Stories Ruthlessly - And Get Value Earlier](/post/split-user-stories-get-value-early/)
