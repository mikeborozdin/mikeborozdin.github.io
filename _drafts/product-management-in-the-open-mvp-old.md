---
layout: product-management-in-the-open
title: "MVP (old)"
permalink: /product-management-in-the-open/mvp-old
comments: true
draft: true
---

In this post we'll talk about defining what an MVP should be. It’s part of [the ‘Product Management in the Open’ series](/product-management-in-the-open). The goal of the series is describe the process of product managing a mobile app - Best in Town / Best in Edinburgh. Please, [check this page to read more about the app itself](/product-management-in-the-open/idea). 

This post describes my thinking back in the beginning of 2017. Not everything I did at that time was correct and I also try to reflect on some of the mistakes here.<!-- more -->

MVP
====

Okay, so in the previous post, we've set a value hypothesis and agreed on the KPIs. Now, it's time to prove or disprove the hypothesis.

When we defined the hypothesis and set the KPI of monthly retention, we made an assumption that we would indeed build a workable product. Of course, there could be alternatives like asking if people liked the idea.

I did speak to a few friends of mine and everyone *seemed* to like the idea. The problem here is that people, especially, if they know you would normally tell that they like your idea. But it doesn't necessarily they actually *need* your product.

Potential Alternatives to Building an MPV
----

Potentially, I could've created an online questionnaire. There I could've asked if people were happy with the existing products - TripAdvisor, Yelp, Google Maps, etc., and if those products have ever failed them. I would avoid asking people directly about my idea, as to avoid a positive bias. But I would lead questions to that.

What MPV Should Have
====

An MPV should be as small enough as it is enough to confirm or discard the hypothesis. No more. Remember, we still need to build it. And any additional time to do that we'll take away valuable time.

Our MVP
====

Let's quickly recap the hypothesis we want to prove and the KPIs we're using for proving that. They will drive the scope of the MPV.

> **Hypothesis:** People need an ability to see ratings for a meal/drink as opposed to the entire place

> **KPI:** Monthly retention is above 20%

So we just need to build an app that allows people to look for specific things and ratings of places for those things. 

More specifically, we need an app that does the following:

* Asks a user to select what the are looking for (coffee, craft beer, wine, etc.)
* Shows places nearby with the rating for that specific thing

|![Choose category](/images/product-management-in-the-open/mvp/choose-category.png)|![See ratings on the map](/images/product-management-in-the-open/mvp/map.png)|![Open place](/images/product-management-in-the-open/mvp/place.png)

And... it has to have content. It's not just enough to build an app. The app needs to have enough data, i.e. places with ratings to be useful for users.

Moreover, here we also need to define which categories are important for an MPV. Indeed, in theory there could be a huge number of potential things people may look - a steak, brunch, plateau de fruits de mer, and so on. We need to identify such categories that people are passionate about and that are possible to add for the MVP. It is important to stress that people have to care about a category. If it's a commodity that any places does good (for example, a light lager), then people are unlikely to use a dedicated app for that. Instead, they can just walk in into any bar and get a light refreshing lager.

On the other hand, there are things people that people are paying quite a bit of money (cocktails) or are just quite choose about (wanted to say snobbish), e.g. coffee.

As a result, I've decided to put the following categories into the MVP:

* Coffee
* Something sweet (a broad category for sweet pastries)
* Craft beer
* Wine 
* Cocktails
* Haggis (a Scottish traditional dish, since first the app was)

What MPV will not have:

* Photos of a place
* Textual reviews
* Ability to change ratings
* Convenient way of adding new places
