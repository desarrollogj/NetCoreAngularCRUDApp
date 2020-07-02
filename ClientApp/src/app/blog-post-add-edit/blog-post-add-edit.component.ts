import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { BlogPostService } from '../services/blog-post.service';
import { BlogCategoryService } from '../services/blog-category.service';
import { BlogPost } from '../models/blogpost';
import { BlogCategory } from '../models/blogcategory';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-blog-post-add-edit',
  templateUrl: './blog-post-add-edit.component.html',
  styleUrls: ['./blog-post-add-edit.component.css']
})
export class BlogPostAddEditComponent implements OnInit {
  form: FormGroup;
  actionType: string;

  formCreator: string;
  formTitle: string;
  formCategory: string;
  formBody: string;
  postId: number;

  enableCreatorEdit: boolean;
  errorMessage: any;
  existingBlogPost: BlogPost;
  blogCategories$: Observable<BlogCategory[]>;

  constructor(private blogPostService: BlogPostService, private blogCategoryService: BlogCategoryService, private formBuilder: FormBuilder,
              private avRoute: ActivatedRoute, private router: Router) {
                const idParam = 'id';
                this.actionType = 'Add';
                this.formCreator = 'creator';
                this.formTitle = 'title';
                this.formCategory = 'category';
                this.formBody = 'body';
                this.enableCreatorEdit = true;

                if (this.avRoute.snapshot.params[idParam]) {
                  this.postId = this.avRoute.snapshot.params[idParam];
                }

                this.form = this.formBuilder.group(
                  {
                    postId: 0,
                    creator: ['', [Validators.required]],
                    title: ['', [Validators.required]],
                    category: ['', [Validators.required]],
                    body: ['', [Validators.required]],
                  }
                );
    }

  ngOnInit(): void {
    this.loadBlogCategories();
    if (this.postId > 0) {
      this.actionType = 'Edit';
      this.enableCreatorEdit = false;
      this.blogPostService.getBlogPost(this.postId)
        .subscribe(data => (
          this.existingBlogPost = data,
          this.form.controls[this.formCreator].setValue(data.creator),
          this.form.controls[this.formTitle].setValue(data.title),
          this.form.controls[this.formCategory].setValue(2), // TODO: Change for value returned by blog post
          this.form.controls[this.formBody].setValue(data.body)
        ));
    }
  }

  loadBlogCategories(): void {
    this.blogCategories$ = this.blogCategoryService.getBlogCategories();
  }

  save(): void {
    if (!this.form.valid) {
      return;
    }

    if (this.actionType === 'Add') {
      const blogPost: BlogPost = {
        dt: new Date(),
        creator: this.form.get(this.formCreator).value,
        title: this.form.get(this.formTitle).value,
        body: this.form.get(this.formBody).value
      };

      this.blogPostService.saveBlogPost(blogPost)
        .subscribe((data) => {
          this.router.navigate(['/blogposts']);
        });
    }

    if (this.actionType === 'Edit'){
      const blogPost: BlogPost = {
        postId: this.existingBlogPost.postId,
        dt: this.existingBlogPost.dt,
        creator: this.existingBlogPost.creator,
        title: this.form.get(this.formTitle).value,
        body: this.form.get(this.formBody).value
      };
      this.blogPostService.updateBlogPost(blogPost.postId, blogPost)
        .subscribe((data) => {
          this.router.navigate(['/blogposts']);
        });
    }
  }

  cancel(): void {
    this.router.navigate(['/']);
  }

  get creator(): AbstractControl { return this.form.get(this.formCreator); }
  get title(): AbstractControl { return this.form.get(this.formTitle); }
  get category(): AbstractControl { return this.form.get(this.formCategory); }
  get body(): AbstractControl { return this.form.get(this.formBody); }
}
