#lang racket
(define (test)
  (define a (random 10))
  (define b (random 10))
  (list a b (+ a b)))
(define (piece str n)
  (substring str 0 n))
(define (twice f v)
  (f (f v)))
(define (len lst)
  (if (empty? lst) 0
      (+ 1 (len (rest lst)))))

;;flatten: pass list
;;returns list with no nested lists
(define (flatten lst)
  (cond [(null? lst) empty]
        [(number? lst) (list lst)]
        [else (append (flatten (car lst)) (flatten (cdr lst)))]))

;;mergesort: pass list
;;returns sorted version of list
(define (mergesort lst)
  (cond [(<= (length lst) 1) lst]
        [else
         (define mid (round (/ (+ (length lst) -0.5) 2)))
         (define right (mergesort (drop lst (inexact->exact mid))))
         (define left (mergesort (take lst (inexact->exact mid))))
         (merge left right)]))
      
;;merge: pass two non-decreasing lists
;;returns merge of two lists in non-decreasing order
(define (merge left right)
  (define merged '())
  (let loop ()
    (when (and (not (empty? left)) (not (empty? right)))
      (cond [(<= (car left) (car right))
             (set! merged (append merged (list (car left))))
             (set! left (cdr left))]
            [else 
             (set! merged (append merged (list (car right))))
             (set! right (cdr right))])
      (loop)))
  (set! merged (append merged (append left right)))
  merged)

;;sorted?: pass list
;;returns if list is in non-decreasing order
(define (sorted? lst)
  (if (<= (length lst) 1) #t
      (and (<= (car lst) (cadr lst))
           (sorted? (cdr lst)))))

;;sortacle: pass algorithm and # of tests
;;returns if alg is valid
(define (sortacle alg testcases)
  (define rlst (randomlst (random 20)))
  (cond [(<= testcases 0) #t]
      [else 
       (define slst (alg rlst))
       (write (sorted? slst))
       (printf ": ")
       (write rlst)
       (printf " -> ")
       (write (alg rlst))
       (printf "\n")
       (and (equal? (list->set rlst) (list->set slst)) ;;check set equality
            (and (sorted? slst)                        ;;check if list is actually sorted
                 (sortacle alg (- testcases 1))))]))

;;randomlst: pass length n
;;return random list of length n
(define (randomlst n)
  (define lst '())
  (let loop()
    (when (> n 1)
      (set! lst (cons (random 100) lst))
      (set! n (- n 1))
    (loop)))
  lst)

(define (nothing l) l)
(define (tricky a) (list 1))