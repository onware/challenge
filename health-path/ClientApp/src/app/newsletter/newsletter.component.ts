import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

import { NewsletterService } from '../newsletter.service';

const ALREADY_SUBSCRIBED = "alreadySubscribed";

@Component({
  selector: 'app-newsletter',
  templateUrl: './newsletter.component.html',
  styleUrls: ['./newsletter.component.css']
})
export class NewsletterComponent implements OnInit {

  email = new FormControl('', [Validators.required, Validators.email]);

  constructor(private newsletterService: NewsletterService) { }

  ngOnInit(): void {
  }

  getErrorMessage() {
    if (this.email.errors?.['required']) {
      return "Email is required";
    } else if (this.email.errors?.['email']) {
      return "Email is in incorrect format";
    } else if (this.email.errors?.[ALREADY_SUBSCRIBED]) {
      return "Email has already been subscribed to the newsletter";
    } else {
      return "";
    }
  }

  subscribe() {
    if (this.email.valid) {
      const email = this.email.value;
      if (email) {
        this.newsletterService.subscribe(email)
          .subscribe({
            next: success => 
            {
              if (!success) {
                this.email.setErrors({[ALREADY_SUBSCRIBED]: true});
              }
            }
          });
      }
    }
  }
}
