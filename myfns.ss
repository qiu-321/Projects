;Name: Orfilia Qiu
;PPL, Project 3

;Test ex.: 
;(myinterpreter '((prog (myadd (myadd 7 (myignore (mymul 4 5))) (mymul 2 5)))))
;=>17

;myinterpreter calls evaluate_prog to evaluate prog
(define (myinterpreter x)
   (cond
      ((null? x) x)
      (#t (cons (evaluate_prog (car x)) (myinterpreter (cdr x))))
   )
)
;for evaluating prog
;When calling evaluate_expr, the function passes an empty binding list
(define (evaluate_prog x)
  (cond
    ((null? x) x)
    (#t (evaluate_expr (cdr x) '(())))
   )
)
;for evaluating expr
;expr can be passed in as () or (()), whichever is fine
(define (evaluate_expr x binding)
   (cond
          ((integer? x) x)
          ((integer? (car x)) (car x))
          ((equal? (car x ) 'myignore) 0)
          ((equal? (car x ) 'myadd) (evaluate_myadd (cdr x) binding))
          ((equal? (car x ) 'myneg) (evaluate_myneg (cdr x) binding))
          ((equal? (car x ) 'mymul) (evaluate_mymul (cdr x) binding))
          ((equal? (car x ) 'mylet) (evaluate_mylet (cdr x) binding))
          ((symbol? (car x )) (searching (car x) binding))
          ((equal? (car (car x )) 'myignore) 0)
          ((equal? (car (car x )) 'myadd) (evaluate_myadd (cdr (car x)) binding))
          ((equal? (car (car x )) 'mymul) (evaluate_mymul (cdr (car x)) binding))
          ((equal? (car (car x )) 'myneg) (evaluate_myneg (cdr (car x)) binding))
          ((equal? (car (car x )) 'mylet) (evaluate_mylet (cdr (car x)) binding))
          ((symbol? (car (car x )) (searching (car (car x)) binding)))
          (#t '(-1))
   )
)
;when evaluate_expr reads a symbol, it uses this searching to search for its binding value.
(define (searching x binding)
  (cond
    ((null? binding) -100)
    ((equal? x 
                (car (car binding)))  (car (cdr (car binding))) )
    (#t (searching x (cdr binding)))
  )
)
;for evaluating myadd
(define (evaluate_myadd x binding)
    (+ (evaluate_expr (cons (car x) '()) binding) (evaluate_expr (cdr x) binding))

)
;for evaluating myneg
(define (evaluate_myneg x binding)
  (* (evaluate_expr (cons (car x) '()) binding) -1)

)
;for evaluating mymul
(define (evaluate_mymul x binding)
    (* (evaluate_expr (cons (car x) '()) binding) (evaluate_expr (cdr x) binding))

)
;for evaluating mylet
(define (evaluate_mylet x binding)
  ;This is doing the update of the binding list
  (let ([binding
         (cons (cons (car x) (cons (evaluate_expr (car (cdr x)) binding) '())) binding)
         ])
        (evaluate_expr  (cdr (cdr x)) binding))

)
  