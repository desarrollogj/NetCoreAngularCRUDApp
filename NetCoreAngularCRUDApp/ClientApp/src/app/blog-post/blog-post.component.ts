import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { BlogPostService } from '../services/blog-post.service';
import { BlogPost } from '../models/blogpost';

@Component({
  selector: 'app-blog-post',
  templateUrl: './blog-post.component.html',
  styleUrls: ['./blog-post.component.css']
})
export class BlogPostComponent implements OnInit {
  blogPost$: Observable<BlogPost>;
  postId: number;

  constructor(private blogPostService: BlogPostService, private avRoute: ActivatedRoute, private router: Router) {
    const idParam = 'id';
    if (this.avRoute.snapshot.params[idParam]) {
      this.postId = this.avRoute.snapshot.params[idParam];
    }
  }

  ngOnInit(): void {
    this.loadBlogPost();
  }

  loadBlogPost(): void {
    this.blogPost$ = this.blogPostService.getBlogPost(this.postId);
  }

  cancel(): void {
    this.router.navigate(['/']);
  }
}
