---
layout: post
title: "Shallow rendering & React Hooks. And why shallow rendering is good"
comments: true
---

Now you can use shallow rendering for testing React components with hooks. And a few words on why shallow rendering is good.

<!-- more -->

Shallow Rendering & React Hooks
====

Up until recently it was tricky to use shallow rendering and libraries like `enzyme` for testing React components that relied on hooks like `useEffect()` and `useLayoutEffect()`. So I've released a library - [jest-react-hooks-shallow](https://github.com/mikeborozdin/jest-react-hooks-shallow) - that brings those hooks to shallow rendering. 

All you need to do is to download the library:

{% highlight bash %} 
npm install --save-dev jest-react-hooks-shallow
# or
yarn add --dev jest-react-hooks-shallow
{% endhighlight %}

and add these lines to your Jest setup file (specified by `setupFilesAfterEnv`):

{% highlight javascript %} 
import enableHooks from 'jest-react-hooks-shallow';

// pass an instance of jest to `enableHooks()`
enableHooks(jest);
{% endhighlight %} 

And voilà - `useEffect()` and `useLayoutEffect()` will work with shallow rendering. From this moment on your test don't need to know anything about `useEffect()`. After all, it's a mere implementation detail.

# Testing

So if you have a component like this:

{% highlight jsx %} 
const ComponentWithHooks = () => {
  const [text, setText] = useState<>();
  const [buttonClicked, setButtonClicked] = useState<boolean>(false);

  useEffect(() => setText(
    `Button clicked: ${buttonClicked.toString()}`), 
    [buttonClicked]
  );

  return (
    <div>
      <div>{text}</div>
      <button onClick={() => setButtonClicked(true)}>Click me</button>
    </div>
  );
};
{% endhighlight %} 

You can easily test it with code like this:

{% highlight jsx %} 
test('Renders default message and updates it on clicking a button', () => {
  const component = shallow(<App />);

  expect(component.text()).toContain('Button clicked: false');

  component.find('button').simulate('click');

  expect(component.text()).toContain('Button clicked: true');
});
{% endhighlight %} 

Please note, that those tests didn't have to import anything else. They simply don't know that a component calls `useEffect()`. Yet, it's being called when you invoke `shallow()`.

That said, often you want to test that a specific function has been called on some event. For example, you're calling a Redux action creator or a Mobx action. If you're using React Hooks, chances are you'll pass that function as a callback to `useEffect()`.

No problems! You can easily test it with simple Jest mocks.

Say, we have a component like this:

{% highlight jsx %} 
import someAction from './some-action';

const ComponentWithHooks = () => {
  const [text, setText] = useState<>();
  const [buttonClicked, setButtonClicked] = useState<boolean>(false);

  useEffect(someAction, [buttonClicked]);

  return (
    <div>
      <div>{text}</div>
      <button onClick={() => setButtonClicked(true)}>Click me</button>
    </div>
  );
};
test('Calls `myAction()` on the first render and on clicking the button`', () => {
  const component = shallow(<App />);
  expect(callback).toHaveBeenCalledTimes(1);

  component.find('button').simulate('click');
  expect(callback).toHaveBeenCalledTimes(2);
});
{% endhighlight %} 

You can find out more about `jest-react-hooks-shallow` [on its Github page](https://github.com/mikeborozdin/jest-react-hooks-shallow).

Why shallow rendering?
====

Some people may say why bring React Hooks to enzyme when there's a trend to use full rendering with libraries like `react-testing-library`. I've even sparked an interesting discussion on that when I posted about `jest-react-hooks-shallow` on [Reddit](https://www.reddit.com/r/javascript/comments/ffcunb/now_you_can_use_shallow_rendering_for_testing/). You may check these two sub-threads: [one](https://www.reddit.com/r/javascript/comments/ffcunb/now_you_can_use_shallow_rendering_for_testing/fjxwyhx/) and [two](https://www.reddit.com/r/javascript/comments/ffcunb/now_you_can_use_shallow_rendering_for_testing/fjxubeh/).

So there are a few good reasons for doing shallow rendering:

No unexpected side-effects
----

Let's say you have the following component hierarchy:

```
ComponentA -> ComponentB -> ComponentC (makes an HTTP request)
```

And you're writing a unit test for `ComponentA`. If you render the entire component tree, you tests may not work as expected because of the HTTP request made by `ComponentC`.

So you either have to mock component `B` - and that would be very similar to doing shallow rendering. Or you would have to mock component `C` or provide a stub backend. But the last two options are hardly ideal because they break encapsulation. Your component `A` has no knowledge of component `C` or any HTTP requests, why would a test for that component require that knowledge?

Test-driven development
----

Shallow rendering also assists with test-driven development. Let's take a previous example, but imagine that component `A` doesn't exist, but you have to write, because you need to wrap component `B` in another component. So it'll be far easier to write tests first for a new component that renders the existing ones, when you don't have to render the entire tree.

Re-usable architecture
----

If you have comprehensive unit tests for your components that don't rely rendering the whole tree, it'll be easier make such components re-usable and even extract them to stand-alone libraries.

A few misconceptions about shallow rendering
====

There are two popular misconceptions about shallow rendering:

* It forces you to test implementation details
* It doesn't test from a user point of view

First of all, it is absolutely true that it is bad to test implementation details and you should test from a user's point of view.

But shallow rendering does not force use to test implementation details. And it does allow you to test from a user's point of view.

There's a famous example of reading and setting React state in unit tests. This is wrong. You don't have to that and you can easily test without it.

Also, testing that your component renders specific child components or passes specific properties is **testing** implementation details, it is actually testing its behaviour. After all, that's what your component does - it renders certain elements on certain conditions and passes data to other components.

Let's have a look at a few examples on how you can test components that have different behaviour:

* If your component's purpose to render a piece of text, it's totally acceptable to test that piece of text is displayed.

{% highlight jsx %} 
const MyComponent = () => (
  <div>My message</div>
);

it('Renders message', () => {
  const component = shallow(<MyComponent />);

  expect(component.text()).toContain('My message');
});
{% endhighlight %}


* If your component displays a child component when a certain property is `true`, then you need to test that it renders that component when the property is `true` and it doesn't when it is `false`
{% highlight jsx %} 
const MyComponent = ({ displayChild }) => (
  <>
    {displayChild && <ChildComponent />}
  </>
);

it('Renders `ChildComponent` when necessary', () => {
  expect(
    shallow(<MyComponent displayChild={false} />)
    .find(ChildComponent)
  )
  .toHaveLength(0);

  expect(
    shallow(<MyComponent displayChild={true} />)
    .find(ChildComponent)
  )
  .toHaveLength(1);
});
{% endhighlight %}
* If a component renders a button and hides another a child component when the button is pressed, then we should simulate pressing on a button and checking that a child component is not there.

{% highlight jsx %} 
const MyComponent = () => {
  cost [displayChild, setDisplayChild] = useState(true);

  return (
    <>
      {displayChild && <ChildComponent />}
      <button onClick={() => setDisplayChild(false)}>Hide child</button>
    </>
  );
};

it('Hides `ChildComponent` after pressing on the button', () => {
  const component = shallow(<MyComponent />);

  expect(component.find(ChildComponent)).toHaveLength(0);

  component.find('button').simulate('click');

  expect(component.find(ChildComponent)).toHaveLength(1);
});
{% endhighlight %}

The last example perfectly illustrates how you can test components from a user point of view and still use shallow rendering.

* If your component passes a certain value to a child component, it's okay to test for that:

{% highlight jsx %} 
const MyComponent = () => {
  cost [accepted, setAccepted] = useState(false);

  return (
    <>
      <button onClick={() => setAccepted(true)}>Accept</button>
      <ChildComponent accepted={accepted} />
    </>
  );
};

it('Passes `accepted` to `ChildComponent` on pressing the button', () => {
  const component = shallow(<MyComponent />);

  expect(component.find(ChildComponent).prop('accepted')).toBeFalse();

  component.find('button').simulate('click');

  expect(component.find(ChildComponent).prop('accepted')).toBeTrue();
});
{% endhighlight %}

Don't forget about end-to-end tests
====

Finally, if you really want to test from a user's standpoint, then make sure that you have a few end-to-tests. They could be time consuming to write and run. But at they can tests the whole system end-to-end including the backend.

Conclusion
====
* Now you can use shallow rendering and `enzyme` for testing React components with hooks
  * Check [jest-react-hooks-shallow](https://github.com/mikeborozdin/jest-react-hooks-shallow) for that
* Shallow rendering has a number of advantages
* It does not force you to write bad tests
* You can test from a user's point of view with shallow rendering
* Don't forget about end-to-end testing
