export interface Answer {
    QuestionId: number;
    ModuleId: number;
    AnswerText: string;
    IsCorrect?: boolean;
    SubmittedAt?: Date;
  }
  