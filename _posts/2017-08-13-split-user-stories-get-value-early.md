---
layout: post
title: "Split User Stories Ruthlessly - And Get Value Earlier"
comments: true
---

Why do you need to split user stories?

Often, you hear that it is important to split user stories. You read articles that say it should be possible to complete a user story within a single sprint, ideally within a few days. That’s entirely accurate. 

>But the main benefit of splitting user stories is that you will deliver value faster!<!-- more -->

Let's take a look at an example. Say, we are developing a time tracking tool. Its main purpose is to analyse time spent on different projects. 

Let’s also assume that we already have a prioritised backlog that looks like this:

* Enter timesheets
* Approve timesheets
* View time spent by project

At the first glance, the backlog looks reasonable. It appears to be prioritised and the user stories look small enough. But the devil is in the details. When you look at the the first story - `‘Enter timesheets’`, you'll see the following:

	As a developer
	I want to be able to log time spent on projects
	So that the company could analyse time spent on different projects

	**Acceptance criteria**

	* Users select a project and enter hours spent for each day
	* Users can add more lines for other projects
		* Users can delete lines for projects
		* If a user presses [delete] on an item with hours, 
			there has to be a confirmation pop-up
	* There should be an area where a user can see total time spent per day
		* We want a user to avoid making mistakes. If time soent is less or more than 
		8 hours per day it has to be coloured in red. 
		If it’s 8 hours plus/minus one hour, it should be coloured in green.

There’s also a mockup attached.

![Mockup](/images/2017-08-13-split-user-stories/mockup.png)

Well, that’s escalated quickly! A simple story has turned out into a collection of business rules!

It may seem that each rule is small enough and can be easily implemented. But don't give in to that temptation!

Each rule is small enough, but in total they all will make up to a substantial amount of time to develop. That will, in turn, require a good amount of testing, as well.

Think of Product Goal
====

So, let’s stop and think. What’s the proposition behind your product? It is analysis of time spent on different projects. There is even a user story for that. So, why don’t we go to that user story as fast as possible?!

Consider Main Goal of User Story. Split Story to Achive That Goal Faster
====

And what is the main premise of the ‘Enter timesheets’ story? Well, that’s an ability to enter timesheets! So, let's focus on that activity and try to move other bits into separate user stories. 

If we look at the acceptance criteria, we'll see that most of it is not about entering timesheets, but related functionality. For example, we want to have a variable number of activities (lines) per week. Then, we would like to see nice visual cues, if hours entered look wrong.

Those are great ideas, but do they help us to achive the overacrching product goal? No. So, let's focus on what is really important. And split the user story into:

* Enter timesheets (3 activities per week)
* Visual cues when the hours entered doesn't look right
* Have more than 3 activities per week

Re-prioritise Backlog
====

Now, we should go back to our backlog and re-prioritise it. We want to put the last two stories to the bottom of the backlog. At the end your backlog should look like:

* Enter timesheets (3 activities per week)
* Approve timesheets
* View time spent by project
* Visual cues when the hours entered doesn't look right
* Have more than 3 activities per week

Always re-prioritise the backlog after splitting user stories. Otherwise, you would do the same things in the initial order and wouldn't move towards your product goals fast enough.

>Splitting user stories allows you to prioritise what is really important. And achieve the product goals faster. 

You’ll probably have to repeat this exercise for ‘Approve Timesheets’ and ‘View time spent by project’. But I guess you’ve got the idea. You'll end up with the **Prioritise - Split - Prioritise cycle**.

![Prioritise-Split Cycle](/images/2017-08-13-split-user-stories/prioritise-split-cycle.png)

Another good way to look at is to revisit the famous picture of the iterative Mona Lisa (pictures below are credit of Jeff Patton - the inventor of User Story Maps) 

**Incremental Mona Lisa**

![Incremental Mona Lisa](/images/2017-08-13-split-user-stories/incremental-mona-lisa.jpg)

**Iterative Mona Lisa**

![Iterative Mona Lisa](/images/2017-08-13-split-user-stories/iterative-mona-lisa.jpg)

Indeed, you don't want to spend time painting the perfect top-left corner of the picture (the original ‘Enter Timesheets’ story). Instead, you want to sketch out the whole piece. That will already give users and stakeholders something to play with. Such early deliver will allow you to get feedback from users and learn from that.

Additional Reading
====
I've focused on the benefits of splitting user stories here. But if you want to know more about techniques for splitting user stories and prioritising backlogs, then I would suggest the following reading:

* [Patterns for Splitting User Stories](http://agileforall.com/patterns-for-splitting-user-stories/)
* [The New User Story Backlog is a Map](http://jpattonassociates.com/the-new-backlog/)
	* User Story Mapping is a fantastic technique for understand how to achieve a user's goals fast
* [Impact Mapping](https://www.impactmapping.org/delivering.html)
	* Impact Mapping is especially good at understand what you **don't need** to do in order to achieve your goals