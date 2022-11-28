# Onware Developer Candidate Challenge

Welcome! We're glad you've taken up our challenge.

## First launch

A postCreateCommand script is running and will take a couple minutes to complete.
While it is running, it will be displayed in the Terminal view in the panel below.

## Getting Started

GitHub codespaces is very similar to Visual Studio Code, but is running in your browser.

If you're not familiar with either Visual Studio Code or codespaces, here's some quick tips:
* To shut down the codespace, use the green button in the bottom toolbar
* The Git graph icon in the feature bar on the left is used to commit changes to your repository.
    * **Important**: you must commit and push your changes in order for them to be saved and shareable
* To run the project, use the Run and Debug feature (Play symbol with bug icon) in the bar on the left.
  * To open the app, use the "Ports" tab in the panel below. You will have to do this twice - the first
    request for the backend, and after the front-end is automatically launched, the front-end.
* The doubled document icon in the bar on the left is the file tree.
* The search icon in the bar on the left is a full-project text search.
* To collapse the sidebar, click on any icon twice.

## The Challenges

We expect that you will complete a minimum of 2 of the following 3 challenges.

### A. Prevent duplicate GMail signups

GMail ignores periods in email addresses; example@gmail.com is equivalent to ex.ample@gmail.com or exam.ple@gmail.com,
and would all be delivered to the same inbox. This has come to the attention of the customers, who want to prevent
duplicate signups for GMail users.

Your task is to change the code such that it is no longer possible to sign up with period-based variations on the same
GMail address.

### B. Add product route to catalog

The product catalog fetches data from the Licensed Natural Health Product Database. The customer would like to add the
route (Oral, Topical, etc.) to the listing.

Your task is to change the code to fetch the route data and display it. The database is accessed using an API; check out
the documentation on the [route endpoint](https://health-products.canada.ca/api/documentation/lnhpd-documentation-en.html#a10).

### C. Fix schedule separation defect

When examining the class schedule for the site, the "Yoga with Bert" is displayed as two separate rows with one schedule
item, instead of as one row with two schedule items.

Your task is to resolve this defect.

## Final steps

After you've completed the challenge, be sure to commit your work to a public repository, and then send a link to the
repository back to us. Thanks!
