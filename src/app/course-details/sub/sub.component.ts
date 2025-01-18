import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-sub',
  templateUrl: './sub.component.html',
  styleUrls: ['./sub.component.css']
})
export class SubComponent {

  topic: any; // Will hold topic details
  topicId!: number;

  constructor(private route: ActivatedRoute, private http: HttpClient) {}

  ngOnInit(): void {
    // Retrieve the topic ID from the route
    this.topicId = Number(this.route.snapshot.paramMap.get('id'));
    this.getTopicDetails(this.topicId);
  }

  // Fetch topic details from API
  getTopicDetails(id: number): void {
    this.http.get(`https://localhost:7243/api/Topic/${id}`).subscribe(
      (data: any) => {
        this.topic = data;
      },
      (error) => {
        console.error('Error fetching topic details:', error);
      }
    );
  }
}
