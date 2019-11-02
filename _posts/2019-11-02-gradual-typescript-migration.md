---
layout: post
title: "How to Gradually move to TypeScript?"
comments: true
---

TypeScript has a lot of advantages. However, it's not often that you are starting a brand new project. More often though you are working on an existing codebase and simply don't have luxury of rewriting everything from scratch. After all, we need to deliver new features and fix annoying bugs. 

But we shouldn't despair - it's possible to gradually move your projects to TypeScript - one module at a time - and run TypeScript and JavaScript side-by-side.

<!-- more -->

Example Project
====

As an example we'll use a simple React & Webpack project. Initially, it'll look like the left-hand side of the diagram - just two modules (`App` and `JsComponent`). And gradually we'll move onto the right-hand side the picture where we'll have JavaScript and TypeScript components side-by-side.

![modules architecture](/images/2019-11-02-typescript/modules.png){: .center-image }

Gradual Migration to TypeScript
====
And in order to keep those languages side-by side, we need to make sure that we can:

* Import TypeScript modules from JavaScript ones
* Import JavaScript modules into TypeScript code

If we can do that, we'll be able to move a project to TypeScript while working on features and bug fixes. 

For example, if you have to add a new React component, you can write it straight in TypeScript. And you'll still be able to use inside other components written in JavaScript.

Initial Webpack setup
===

Since we use Webpack as a module bundler, I want to show its configuration for the pure JavaScript project. That will allow us to see what we started with and what we've added to enable the TypeScript support.

**webpack.config.js**
{% highlight javascript lineanchors %}
const HtmlWebPackPlugin = require('html-webpack-plugin');
const path = require('path');

module.exports = (env) => ({
  entry: {
    index: ['./src/index.jsx'],
  },
  output: {
    path: path.resolve('./dist/'),
  },
  devtool: env === 'production' ? false : 'source-map',
  module: {
    rules: [
      { enforce: 'pre', test: /\.js$/, loader: 'source-map-loader' },
      {
        test: /\.(js|jsx)$/,
        exclude: /node_modules/,
        use: {
          loader: 'babel-loader',
        },
      },
      { test: /\.(ts|tsx)$/, loader: 'awesome-typescript-loader' },
    ],
  },
  resolve: {
    extensions: ['.js', '.jsx', '.ts', '.tsx'],
  },
  plugins: [
    new HtmlWebPackPlugin({ template: './src/index.html' }),
  ],
});

{% endhighlight %}

As you can see it's a very simple setup. The main thing is the use of the `babel-loader` for loading of ES modules with JSX in them.

Import TypeScript modules into JavaScript ones
===

Okay, we're adding a new React component and have an opportunity to convert an existing one into TypeScript. This is a perfect example of importing TypeScript modules into JavaScript ones.

Let's start doing this by adding TypeScript and types for React to our project:

{% highlight bash lineanchors %}
npm install --save-dev typescript @types/react
{% endhighlight %}

Next step would be to `tsconfig.json` to the root folder of the project to configure the TypeScript compiler. The important parts of the configuration are:

* `esModuleInterop: true` - we won't be able to import the React module without it
* `sourceMap: true` - enables source map for debugging

**tsconfig.json**
{% highlight json lineanchors %}
{
  "compilerOptions": {
    "outDir": "./dist/",
    "sourceMap": true,
    "noImplicitAny": true,
    "module": "commonjs",
    "target": "es6",
    "jsx": "react",
    "esModuleInterop": true,
    "strictNullChecks": true
  },
  "include": [
    "./src/**/*"
  ],
  "rules": {
    "quotemark": [
      true,
      "single"
    ]
  }
}
{% endhighlight %}

After this we can write our TypeScript component

**src/js-component/ts-component/ts-component.tsx**
{% highlight bash javascript %}
import React from 'react';

interface Props {
  myProp: number;
}

const TsComponent: React.FC<Props> = ({ myProp }) => (
  <div>
    Hi, I am a TypeScript component and I accept a number prop which is {myProp}
  </div>
);

export default TsComponent;
{% endhighlight %}

Now, if we try to import it from a JavaScript component:

**src/js-component/js-component.tsx**
{% highlight bash javascript %}
import React from 'react';
import TsComponent from './ts-component/ts-component';

const JsComponent = () => (
  <>
    <div>Hi, I am a JavaScript component and I am calling a TypeScript component</div>
    <TsComponent myProp={123} />
  </>
);

export default JsComponent;
{% endhighlight %}

We'll get an error that Webpack is unable to resolve module `ts-component/ts-component`. Indeed, if we look at our Webpack configuration above, we'll see that we only instruct it to resolve `.js` and `*.jsx` files. Adding the `.tsx` to that list will not solve the problem - the `babel-loader` won't be able to parse the TypeScript syntax.

Luckily, we can solve this problem by adding the [awesome-typescript-loader](https://www.npmjs.com/package/awesome-typescript-loader/)

So we just type 

{% highlight bash javascript %}
npm install --save-dev awesome-typescript-loader
{% endhighlight %} 

in the terminal and add the following configuration to `webpack.config.js`:

**webpack.config.js**
{% highlight bash javascript %}  
  module: {
    rules: [
    // existing rules...

    // new TypeScript configuration
    { test: /\.(ts|tsx)$/, loader: 'awesome-typescript-loader' },
    ]
  },
  resolve: {
    // we also need to add *.tsx here
    extensions: ['.js', '.jsx', '.ts', '.tsx'],
  }
{% endhighlight %} 

Now, if we run the code - it'll work flawlessly!

Congratulations! We've added our first TypeScript module into an existing JavaScript project! And more importantly, it didn't require a rewrite of the whole project.

How did that work?
==
You may wonder how we've managed to import a module written in one language into another? Especially, given that language - TypeScript - has more features than the language we've imported it into - JavaScript.

That was possible thanks to Webpack loaders that transform an imported module on the fly into JavaScript. You can read more about them [here](https://webpack.js.org/concepts/loaders/).

But do you have to use Webpack? No! You can use whatever module bundler including Browserify. The main thing is to be enable transformation of imported modules on the fly into JavaScript.

What about debugging?
==
Can you still you the Chrome Debugger? Yes, you still can! First of all, the `tsconfig.json` we've created instructs the TypeScript compilier to produce source maps. Then the initial `webpack.config.js` already included source maps. In the Chrome Debugger you step straight into TypeScript code!

Importing a JavaScript Module into TypeScript
====

Let's have a look at the opposite example - importing a JavaScript module into TypeScript. In this case, I'm talking about importing an own module, as opposed to a 3rd party library, but some of the techniques are the same.

Also, if we're talking about importing an own JavaScript module into TypeScript, why would you do that in the first place? Well, if you have a large JavaScript codebase and you're working on a TypeScript module you may want to re-use your existing work in JavaScript without rewriting it in TypeScript.

Let's add a simple JavaScript component:

**src/ts-component/another-js-component/another-js-component.jsx**
{% highlight bash javascript %}  
import React from 'react';
import PropTypes from 'prop-types';

const AnotherJsComponent = ({ a, b }) => (
  <div>
    Hi, I am another JavaScript components. And these are my properties:
    a: {a} & b: {b}
  </div>
);

AnotherJsComponent.propTypes = {
  a: PropTypes.number.isRequired,
  b: PropTypes.string.isRequired
};

export default AnotherJsComponent;
{% endhighlight %} 

And we can simply import it into our TypeScript component: 

**src/ts-component/ts-component.tsx**
{% highlight bash javascript %} 
import React from 'react';
import AnotherJsComponent from './another-js-component/another-js-component';

interface Props {
  myProp: number;
}

const TsComponent: React.FC<Props> = ({ myProp }) => (
  <>
    <div>
      Hi, I amm a TypeScript component and I accept a number prop which is {myProp}
    </div>
    <AnotherJsComponent a={1} b='foo' />
  </>
);

export default TsComponent;
{% endhighlight %} 

It's all nice and easy, we don't need to change anything in the Webpack configuration. We can simply import JavaScript modules into the TypeScript code.

But are we missing anything? 

Yes, we do miss type-safety! We don't know the types of the component properties we need to pass. 

Is there a way to solve it without having to re-write `AnotherJsComponent` in TypeScript? Yes, we can provide our own type definitions while leaving the implementation of the JavaScript module intact. 

That's exactly the approach that is used when dealing with 3rd party libraries - they either come with their own type definition files or we install them separately. In fact, we've already done that for React when we called `npm install --save-dev @types/react`.

What do we need to do to create our own type definition? 

First, we need to create a file in the same folder as the imported module - `another-js-component.d.ts` and place the following contents there:

**src/ts-component/another-js-component/another-js-component.d.ts**
{% highlight bash javascript %} 
import React from 'react';

interface Props {
  a: number;
  b: string;
}

declare const AnotherJsComponent: React.FC<Props>;

export default AnotherJsComponent;
{% endhighlight %} 

Don't Start with Renaming `.js` files into `.ts`
====
Some migration guides recommend you to start with renaming your `.js` files into `.ts`. That may sound reasonable, after all isn't TypeScript a superset of JavaScript? And should not existing JavaScript code be valid TypeScript code, as well?

Well, not so fast. Imagine, you have the following module:

{% highlight javascript lineanchors %}
const add = (a, b) => a + b;

export default add;

{% endhighlight %}

If we just change the file extention to `.ts`, we will get a TypeScript error. It will tell us that the input arguments `a` and `b` has an implicit type of `any`. And that will be completely reasonable. After all, it's the main purpose of TypeScript to provide static typing. 

Now imagine if you're doing it on the whole codebase - it'll be full of TypeScript errors. Of course, you can set `"noImplicitAny": false` in your `tsconfig.json`. But that will not help you to prevent errors in legit TypeScript files.

Code
====
All the code is available on [GitHub](https://github.com/mikeborozdin/react-typescript-and-javascript-side-by-side).

Conclusion
====

* You don't have to move your projects to TypeScript in one big bang
* Instead, you can move one module at a time
* It's easy to import TypeScript code into JavaScript and vice versa
* You can provide static types around JavaScript modules without having to rewrite them
* Thanks to types definitions files
* You also don't have to use Webpack. All you need is to be able to transform on the fly imported TypeScript modules into JavaScript 
