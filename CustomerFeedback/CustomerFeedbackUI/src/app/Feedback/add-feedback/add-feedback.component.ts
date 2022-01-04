import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FeedbackService } from 'src/app/Shared/feedback.service';
import { WhiteSpaceValidator } from 'src/app/Shared/whitespace.validator';
import { ActivatedRoute } from '@angular/router';
import { feedbackUrl } from 'src/environments/environment';

@Component({
  selector: 'app-add-feedback',
  templateUrl: './add-feedback.component.html',
  styleUrls: ['./add-feedback.component.css']
})
export class AddFeedbackComponent implements OnInit,OnChanges {

  categories : any = [];
  reviews : any = [];
  products : any = [];
  public formValue : FormGroup;
  selectedCategory : any;
  private selectedReview : string = "Unsatisfied";
  checkBoxValue : any;
  selectedFile : File = null;
  feedbackValues: [];
  encryptedValue : string;
  formDisable = false;

  constructor(private service : FeedbackService,private formBuilder : FormBuilder,private router : ActivatedRoute) { }

  ngOnInit(): void {

    this.service.listCategory().subscribe( arr => {
      this.categories = arr;
    });

    this.service.listReview().subscribe(review => {
      this.reviews = review;
    })

    this.getCategoryById(this.selectedCategory);

    this.formValue = this.formBuilder.group({
      title : [0,Validators.required],
      firstName : ['',[Validators.required,WhiteSpaceValidator.noWhiteSpace]],
      initial : ['',[Validators.required,WhiteSpaceValidator.noWhiteSpace]],
      email : ['',[Validators.required,WhiteSpaceValidator.noWhiteSpace]],
      streetAddress : ['',[Validators.required,WhiteSpaceValidator.noWhiteSpace]],
      streetAddress2 : [''],
      city : ['',[Validators.required,WhiteSpaceValidator.noWhiteSpace]],
      region : ['',[Validators.required,WhiteSpaceValidator.noWhiteSpace]],
      postalCode : ['',[Validators.required,WhiteSpaceValidator.noWhiteSpace]],
      country : ['',[Validators.required,WhiteSpaceValidator.noWhiteSpace]],
      productId : ['',Validators.required],
      reviewId : ['',Validators.required],
      lastPurchasedItem : [this.checkBoxValue],
      productFeedback : ['',[Validators.required,WhiteSpaceValidator.noWhiteSpace]],
      reasonForUnSatisfaction : [''],
      fileUpload : ['',Validators.required], 
    })

    this.encryptedValue =  this.router.snapshot.paramMap.get('id');

    if(this.encryptedValue){
      this.service.getFeedback(this.encryptedValue).subscribe(feedback => {
        this.feedbackValues = feedback;
        
      this.formValue.controls['firstName'].setValue(feedback.firstName);
      this.formValue.controls['initial'].setValue(feedback.initial);
      this.formValue.controls['email'].setValue(feedback.email);
      this.formValue.controls['streetAddress'].setValue(feedback.streetAddress);
      this.formValue.controls['streetAddress2'].setValue(feedback.streetAddress2);
      this.formValue.controls['city'].setValue(feedback.city);
      this.formValue.controls['region'].setValue(feedback.region);
      this.formValue.controls['postalCode'].setValue(feedback.postalCode);
      this.formValue.controls['country'].setValue(feedback.country);
      this.formValue.controls['reasonForUnSatisfaction'].setValue(feedback.reasonForUnSatisfaction);
      this.formValue.controls['productFeedback'].setValue(feedback.productFeedback);
      this.formValue.controls['title'].setValue(feedback.title);
      this.formValue.controls['lastPurchasedItem'].setValue(feedback.lastPurchasedItem);
      this.formValue.controls['reviewId'].setValue(feedback.reviewId);
      this.formValue.controls['productId'].setValue(feedback.productId);

      this.formValue.disable();

      })
    }
  }

  ngOnChanges() {
      this.getCategoryById(this.selectedCategory);
      console.log(this.selectedCategory);
  }

  addFeedback() : void{

    this.formDisable = false;

      const fileData = new FormData();
      fileData.append('fileUpload',this.selectedFile);
      fileData.append('lastPurchasedItem',this.checkBoxValue);
      fileData.append('title',this.formValue.get('title').value);
      fileData.append('firstName',this.formValue.get('firstName').value);
      fileData.append('initial',this.formValue.get('initial').value);
      fileData.append('email',this.formValue.get('email').value);
      fileData.append('streetAddress',this.formValue.get('streetAddress').value);
      fileData.append('streetAddress2',this.formValue.get('streetAddress2').value);
      fileData.append('city',this.formValue.get('city').value);
      fileData.append('region',this.formValue.get('region').value);
      fileData.append('postalCode',this.formValue.get('postalCode').value);
      fileData.append('country',this.formValue.get('country').value);
      fileData.append('productId',this.formValue.get('productId').value);
      fileData.append('reviewId',this.formValue.get('reviewId').value);
      fileData.append('productFeedback',this.formValue.get('productFeedback').value);
      fileData.append('reasonForUnSatisfaction',this.formValue.get('reasonForUnSatisfaction').value);

      console.log(fileData);

      this.service.createFeedback(fileData)
         .subscribe((data) => alert("Feedback Submitted Successfully"));

      this.formValue.reset();
  }

  getCategoryId(event : any)
  {
    this.selectedCategory = event.target.value;
    this.getCategoryById(this.selectedCategory);
    console.log(this.selectedCategory);
  }

  reset()
  {
    this.formValue.reset();
  }

  getCategoryById(selectedValue : any) : void
  {
      this.service.getProductByCategory(selectedValue).subscribe(
        (products : any) => {
          this.products = products;
        }
      )
  }

  setRadio(e : string)
  {
    this.selectedReview = e;
  }

  isSelected(name : string) : boolean
  {
    if(this.selectedReview  && this.selectedReview == 'Very Unsatisfied'){
      return true;
    }

    return (this.selectedReview == name);
  }

  checkValue(event : any)
  {
    this.checkBoxValue = event.target.value;
    console.log(this.checkBoxValue);
  }

  onFileChanged(event : any){
     this.selectedFile = <File>event.target.files[0];
     console.log(event);
  }
}
