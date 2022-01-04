import { Component, OnInit } from '@angular/core';
import { FeedbackService } from 'src/app/Shared/feedback.service';

@Component({
  selector: 'app-display-feedback',
  templateUrl: './display-feedback.component.html',
  styleUrls: ['./display-feedback.component.css']
})
export class DisplayFeedbackComponent implements OnInit {

  feedbackDetails = [];
  text : string

  constructor(private service : FeedbackService) { }

  ngOnInit(): void {
    this.service.getFeedback(this.text).subscribe( arr => {
      this.feedbackDetails = arr;
      console.log(this.feedbackDetails);
    });
  }



}
